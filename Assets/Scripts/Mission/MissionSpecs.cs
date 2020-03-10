using System;
using UnityEngine;

namespace LevelManagement.Missions
{
    [Serializable]
    public class MissionSpecs
    {
        [SerializeField] protected string name;
        [SerializeField] [Multiline] protected string description;
        [SerializeField] protected string sceneName;
        [SerializeField] protected string id;
        [SerializeField] protected Sprite sceneImage;

        public string Name => name;
        public string Description => description;
        public string SceneName => sceneName;
        public string ID => id;
        public Sprite SceneImage => sceneImage;
    } 
}
