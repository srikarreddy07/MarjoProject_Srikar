using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class SettingMenu : Menu<SettingMenu>
    {
        [Header("Script")]
        [SerializeField] DataManager dataManager;

        [Header("Sliders")]
        [SerializeField] Slider masterVolumeSlider;

        protected override void Awake()
        {
            base.Awake();
            dataManager = Object.FindObjectOfType<DataManager>();
        }

        private void Start()
        {
            LoadData();
        }

        public void OnMasterVolumeChanged(float volume)
        {
            if (dataManager != null)
                dataManager.MasterVolume = volume;
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            dataManager.Save();
        }


        public void LoadData()
        {
            if (dataManager == null || masterVolumeSlider == null)
                return;

            dataManager.Load();

            masterVolumeSlider.value = dataManager.MasterVolume;
        }
    } 
}
