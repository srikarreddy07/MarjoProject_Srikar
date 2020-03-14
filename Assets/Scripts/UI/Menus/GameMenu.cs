using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class GameMenu : Menu<GameMenu>
    {
        [Header("UI Components")]
        [SerializeField] Slider healthSlider;
        [SerializeField] Text killCount;

        [Header("Script")]
        [SerializeField] PlayerHealth playerHealth;

        protected override void Awake()
        {
            base.Awake();
            playerHealth = Object.FindObjectOfType<PlayerHealth>();
        }

        private void Start()
        {
            LoadData();
        }

        public void OnPausePressed ()
        {
            if(DataManager.Instance != null)
                DataManager.Instance.Save();

            Time.timeScale = 0;
            PauseMenu.Open();
        }

        private void Update()
        {
            if (DataManager.Instance == null || healthSlider == null || killCount == null)
                return;

            healthSlider.value = DataManager.Instance.PlayerHealth;
            killCount.text = DataManager.Instance.PlayerKillCount.ToString();
        }

        public void LoadData()
        {
            if (DataManager.Instance == null || healthSlider == null || killCount == null)
                return;

            DataManager.Instance.Load();

            healthSlider.value = DataManager.Instance.MasterVolume;
            killCount.text = DataManager.Instance.PlayerKillCount.ToString();
        }
    } 
}
