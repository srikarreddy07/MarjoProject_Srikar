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

            DataManager.Instance.Save();
            LevelLoader.LoadNextLevel();
        }

        public void OnRestartPressed ()
        {
            base.OnBackPressed();

            DataManager.Instance.Save();
            LevelLoader.Reload();
        }

        public void OnMainMenuPressed ()
        {
            LevelLoader.LoadLevel(mainMenuIndex);

            DataManager.Instance.Save();
            MainMenu.Open();
        }
    } 
}
