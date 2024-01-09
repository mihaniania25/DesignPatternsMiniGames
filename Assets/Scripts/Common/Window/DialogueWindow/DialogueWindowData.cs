using System;

namespace DesignPatternsMiniGames.Common
{
    public class DialogueWindowData : WindowData
    {
        public string Title;
        public string Description;
        public Action OnAccept;
        public Action OnCancel;
    }
}