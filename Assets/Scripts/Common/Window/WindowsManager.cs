using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DesignPatternsMiniGames.Utility;

namespace DesignPatternsMiniGames.Common
{
    public class WindowsManager : MonoBehaviour
    {
        public event Action<Window> OnWindowOpened;
        public event Action<Window> OnWindowClosed;
        public event Action OnAllWindowsClosed;

        [SerializeField] private Transform _windowsContainer;
        [SerializeField] private Button _substrate;

        private WindowsConfig _windowsConfig;

        private Window _topWindow;
        private List<Window> _createdWindows = new List<Window>();
        private Dictionary<Window, WindowData> _activeWindowsData = new Dictionary<Window, WindowData>();

        public int ActiveWindowsCount => _activeWindowsData.Count;

        private AppConfigs _configs => AppContext.AppConfigs;

        private void Awake()
        {
            _windowsConfig = _configs.WindowsConfig;
            _substrate.onClick.AddListener(OnSubstrateClick);
        }

        private void OnSubstrateClick()
        {
            if (_topWindow != null && _topWindow.CloseableViaSubstrate)
                _topWindow.Close();
        }

        public Window OpenWindow(WindowType windowType, WindowData windowData = null)
        {
            Window winToOpen = GetWindow(windowType);

            if (winToOpen != null)
            {
                bool isWinActive = _activeWindowsData.ContainsKey(winToOpen);

                if (isWinActive)
                {
                    GameLog.Error("[WindowsManager] opening multiple same windows is not supported");
                    return null;
                }

                HandleNewWindow(winToOpen, windowData);
                OnWindowOpened?.Invoke(winToOpen);

                return winToOpen;
            }

            return null;
        }

        private Window GetWindow(WindowType windowType)
        {
            Window window = _createdWindows.Find(w => w.WindowType == windowType);

            if (window == null)
            {
                GameObject windowPrefab = _windowsConfig.GetWindowPrefab(windowType);

                if (windowPrefab != null)
                {
                    GameObject windowGO = Instantiate(windowPrefab, _windowsContainer);
                    windowGO.SetActive(false);
                    window = windowGO.GetComponent<Window>();
                    _createdWindows.Add(window);
                }
            }

            return window;
        }

        private void HandleNewWindow(Window newWindow, WindowData data)
        {
            if (newWindow != null)
            {
                HandleNewTopWindow(newWindow);

                _activeWindowsData.Add(newWindow, data);
                newWindow.OnClose += OnWindowClose;
                newWindow.Open(data);

                RefreshView();
            }
        }

        private void HandleNewTopWindow(Window newTopWindow)
        {
            if (_topWindow != null)
            {
                if (_topWindow.IsOpened && _topWindow.Hideable)
                    _topWindow.Hide();
            }

            _topWindow = newTopWindow;
        }

        private void OnWindowClose(Window window)
        {
            window.OnClose -= OnWindowClose;
            _activeWindowsData.Remove(window);

            if (window == _topWindow)
                OnTopWindowClose();
            OnWindowClosed?.Invoke(window);
        }

        private void OnTopWindowClose()
        {
            Window lastActiveWindow = null;

            if (_activeWindowsData.Count > 0)
            {
                lastActiveWindow = _activeWindowsData.Keys.Last();
                if (lastActiveWindow.IsOpened == false)
                    lastActiveWindow.Open(_activeWindowsData[lastActiveWindow]);
            }

            HandleNewTopWindow(lastActiveWindow);
            RefreshView();
        }

        private void RefreshView()
        {
            UpdateSorting();
            _substrate.gameObject.SetActive(_topWindow != null);
        }

        private void UpdateSorting()
        {
            foreach (Window window in _activeWindowsData.Keys)
            {
                if (window.IsOpened)
                    window.transform.SetAsLastSibling();
            }

            int lastWindowChildIndex = _windowsContainer.childCount - 1;
            _substrate.transform.SetSiblingIndex(lastWindowChildIndex - 1);
        }

        public void CloseAll()
        {
            foreach (Window window in _activeWindowsData.Keys)
            {
                window.OnClose -= OnWindowClose;
                if (window.IsOpened)
                    window.Close();
            }

            _substrate.gameObject.SetActive(false);
            _topWindow = null;

            _activeWindowsData.Clear();

            OnAllWindowsClosed?.Invoke();
        }

        private void OnDestroy()
        {
            CloseAll();
            _substrate.onClick.RemoveListener(OnSubstrateClick);
        }
    }
}