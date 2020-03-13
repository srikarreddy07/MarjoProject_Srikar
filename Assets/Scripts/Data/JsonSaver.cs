using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace LevelManagement.Data
{
    public class JsonSaver
    {
        private static readonly string fileName = "JsonFile.sav";

        public static string GetSaveFileName()
        {
            return Application.persistentDataPath + "/" + fileName;
        }

        public void Save(SaveData data)
        {
            data.hashValue = string.Empty;

            string json = JsonUtility.ToJson(data);
            string saveFileName = GetSaveFileName();

            data.hashValue = GetSha256(json);
            json = JsonUtility.ToJson(data);

            FileStream fileStream = new FileStream(saveFileName, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }

        public bool Load(SaveData data)
        {
            string loadFileName = GetSaveFileName();

            if (File.Exists(loadFileName))
            {
                using (StreamReader reader = new StreamReader(loadFileName))
                {
                    string json = reader.ReadToEnd();

                    if (CheckData(json))
                        JsonUtility.FromJsonOverwrite(json, data);
                    else
                        Debug.LogWarning("You have being hacked");
                }
                return true;
            }
            return false;
        }

        public bool CheckData(string json)
        {
            SaveData tempSaveData = new SaveData();
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            string oldHash = tempSaveData.hashValue;
            tempSaveData.hashValue = string.Empty;

            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSha256(tempJson);

            return (oldHash == newHash);
        }

        public void Delete()
        {
            File.Delete(GetSaveFileName());
        }

        public string GetHexStringFromHash(byte[] hash)
        {
            string hextString = string.Empty;

            foreach (byte b in hash)
            {
                hextString += b.ToString("X2");
            }
            return hextString;
        }

        public string GetSha256(string text)
        {
            byte[] textToByte = Encoding.UTF8.GetBytes(text);

            SHA256Managed sha256Managed = new SHA256Managed();

            byte[] hashValue = sha256Managed.ComputeHash(textToByte);

            return GetHexStringFromHash(hashValue);
        }
    } 
}
