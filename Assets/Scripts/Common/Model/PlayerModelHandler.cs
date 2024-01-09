using System.IO;
using UnityEngine;
using DesignPatternsMiniGames.Utility;

namespace DesignPatternsMiniGames.Common
{
    public class PlayerModelHandler
    {
        private const string FILE_NAME = "Save.json";

        private string _path;

        public PlayerModel Model { get; private set; }

        public PlayerModelHandler()
        {
            _path = Application.persistentDataPath + "/" + FILE_NAME;
        }

        public void Init()
        {
            LoadModel();

            InitModel();

            SaveModel();
        }

        private void LoadModel()
        {
            if (File.Exists(_path))
                Model = FileSerializeHelper.LoadFromFile<PlayerModel>(_path);
        }

        private void InitModel()
        {
            Model ??= new PlayerModel();
            Model.Init();
        }

        public void SaveModel()
        {
            FileSerializeHelper.SaveToFile(Model, _path);
        }
    }
}