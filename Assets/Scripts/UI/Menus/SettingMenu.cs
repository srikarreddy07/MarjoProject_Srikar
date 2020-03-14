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

        public override void OnBackPressed()
        {
            base.OnBackPressed();

            DataManager.Instance.Save();
        }


        public void LoadData()
        {
            if (DataManager.Instance == null || masterVolumeSlider == null)
                return;

            DataManager.Instance.Load();

            masterVolumeSlider.value = DataManager.Instance.MasterVolume;
        }
    } 
}
