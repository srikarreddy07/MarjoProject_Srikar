using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Missions
{
    public class MissionSelector : MonoBehaviour
    {
        [SerializeField] protected MissionsList missionsList;

        protected int currentIndex = 0;
        public int CurrentIndex => currentIndex;

        public void ClampIndex ()
        {
            if(missionsList.TotalMissions == 0)
            {
                Debug.LogWarning("Mission data error!");
                return;
            }

            if(CurrentIndex >= missionsList.TotalMissions)
            {
                currentIndex = 0;
            }

            if(CurrentIndex < 0)
            {
                currentIndex = missionsList.TotalMissions - 1;
            }
        }

        public void SetIndex (int index)
        {
            currentIndex = index;
            ClampIndex();
        }

        public void IncrementIndex ()
        {
            SetIndex(currentIndex + 1);
        }

        public void DecementIndex ()
        {
            SetIndex(currentIndex - 1);
        }

        public MissionSpecs GetMission (int index)
        {
            return missionsList.GetMission(index);
        }

        public MissionSpecs GetCurrentMissionSpecs ()
        {
            return missionsList.GetMission(currentIndex);
        }
    } 
}
