using UnityEngine;

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        [Header("Components")]
        private SaveData saveData;
        private JsonSaver jsonSaver;

        #region Properties
        public float MasterVolume
        {
            get { return saveData.masterVolume; }
            set { saveData.masterVolume = value; }
        }

        public float PlayerHealth
        {
            get { return saveData.healthValue; }
            set { saveData.healthValue = value; }
        }

        public int PlayerKillCount
        {
            get { return saveData.killCount; }
            set { saveData.killCount = value; }
        }
        #endregion


        private void Awake()
        {
            saveData = new SaveData();
            jsonSaver = new JsonSaver();
        }

        public void Save ()
        {
            jsonSaver.Save(saveData);
        }

        public void Load()
        {
            jsonSaver.Load(saveData);
        }
    } 
}
