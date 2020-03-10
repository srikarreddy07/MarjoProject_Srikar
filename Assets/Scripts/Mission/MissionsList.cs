using UnityEngine;
using System.Collections.Generic;

namespace LevelManagement.Missions
{
    [CreateAssetMenu(fileName ="Missions List", menuName ="Mission/MissionList", order = 1)]
    public class MissionsList : ScriptableObject
    {
        [SerializeField] List<MissionSpecs> missions;
        public int TotalMissions => missions.Count;

        public MissionSpecs GetMission (int index)
        {
            return missions[index];
        }
    }
}
