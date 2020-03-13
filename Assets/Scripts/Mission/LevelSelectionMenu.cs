using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Missions;

namespace LevelManagement
{
    [RequireComponent(typeof(MissionSelector))]
    public class LevelSelectionMenu : Menu<LevelSelectionMenu>
    {
        [SerializeField] protected Text levelName;
        [SerializeField] protected Text levelDescription;
        [SerializeField] protected Image levelImage;

        protected MissionSelector missionSelector;
        protected MissionSpecs currentMission;

        protected override void Awake ()
        {
            base.Awake();
            missionSelector = GetComponent<MissionSelector>();

            UpdateInfo ();
        }

        private void OnEnable ()
        {
            UpdateInfo ();
        }

        public void UpdateInfo ()
        {
            currentMission = missionSelector.GetCurrentMissionSpecs();

            levelName.text = currentMission?.Name;
            levelDescription.text = currentMission?.Description;
            levelImage.sprite = currentMission?.SceneImage;
            levelImage.color = Color.white;
        }

        public void OnNextPressed ()
        {
            missionSelector.IncrementIndex();
            UpdateInfo();
        }

        public void OnPreviousPressed ()
        {
            missionSelector.DecementIndex();
            UpdateInfo();
        }

        public void OnPlayPressed ()
        {
            LevelLoader.LoadLevel(currentMission?.SceneName);
            GameMenu.Open();
        }
    } 
}
