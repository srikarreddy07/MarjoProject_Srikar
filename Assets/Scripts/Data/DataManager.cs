using UnityEngine;

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        [Header("Components")]
        private SaveData saveData;
        private JsonSaver jsonSaver;

        private static DataManager instance;

        #region Properties
        public static DataManager Instance { get => instance;}

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
            if (instance != null)
                Destroy(gameObject);
            else
                instance = this;

            saveData = new SaveData();
            jsonSaver = new JsonSaver();
        }

        private void OnDestroy()
        {
            instance = null;
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
