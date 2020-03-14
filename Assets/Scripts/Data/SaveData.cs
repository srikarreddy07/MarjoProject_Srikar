using System;

namespace LevelManagement.Data
{
    [Serializable]
    public class SaveData
    {
        public string hashValue;

        public float masterVolume;

        public float healthValue;
        public int killCount;

        public SaveData()
        {
            hashValue = string.Empty;

            masterVolume = 0f;
            healthValue = 100f;
            killCount = 0;
        }
    } 
}
