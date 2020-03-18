using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class SettingMenu : Menu<SettingMenu>
    {
        //[Header("Script")]
        //[SerializeField] DataManager dataManager;

        [Header("Sliders")]
        [SerializeField] Slider masterVolumeSlider;

        protected override void Awake()
        {
            base.Awake();
            //dataManager = Object.FindObjectOfType<DataManager>();
        }

        private void Start()
        {
            LoadData();
        }

        public void OnMasterVolumeChanged(float volume)
        {
            if (DataManager.Instance != null)
                DataManager.Instance.MasterVolume = volume;
        }

        public void OnHealthPressed ()
        {
            if (DataManager.Instance != null)
            {
                DataManager.Instance.PlayerHealth = 100;
                DataManager.Instance.Save();
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            if (DataManager.Instance != null)
                DataManager.Instance.Save();
        }


        public void LoadData()
        {
            if (DataManager.Instance == null || masterVolumeSlider == null)
                return;

            DataManager.Instance.Load();

            masterVolumeSlider.value = DataManager.Instance.MasterVolume;
            DataManager.Instance.PlayerHealth = 100;
        }
    } 
}
