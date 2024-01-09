using System;
using System.IO;
using UnityEngine;

namespace DesignPatternsMiniGames.Utility
{
    public static class FileSerializeHelper
    {
        public static void SaveToFile(object obj, string path)
        {
            string json = JsonUtility.ToJson(obj);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(json);
                }
            }
        }

        public static T LoadFromFile<T>(string path)
        {
            try
            {
                string json;

                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        json = streamReader.ReadToEnd();
                    }
                }

                T objFromJson = JsonUtility.FromJson<T>(json);
                return objFromJson;
            }
            catch(Exception e)
            {
                GameLog.Error("[FileSerializeHelper]: " + e.Message);
            }

            return default;
        }
    }
}