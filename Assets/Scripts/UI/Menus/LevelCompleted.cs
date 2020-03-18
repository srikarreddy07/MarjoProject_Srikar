using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Data;

namespace LevelManagement
{
    public class LevelCompleted : Menu<LevelCompleted>
    {
        [SerializeField] int mainMenuIndex;

        public void OnNextPressed ()
        {
            base.OnBackPressed();

            if(DataManager.Instance)
                DataManager.Instance.Save();
            LevelLoader.LoadNextLevel();
        }

        public void OnRestartPressed ()
        {
            base.OnBackPressed();

            if (DataManager.Instance)
                DataManager.Instance.Save();
            LevelLoader.Reload();
        }

        public void OnMainMenuPressed ()
        {
            LevelLoader.LoadLevel(mainMenuIndex);

            if (DataManager.Instance)
                DataManager.Instance.Save();
            MainMenu.Open();
        }
    } 
}
