using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class LevelCompleted : Menu<LevelCompleted>
    {
        [SerializeField] int mainMenuIndex;

        public void OnNextPressed ()
        {
            base.OnBackPressed();

            LevelLoader.LoadNextLevel();
        }

        public void OnRestartPressed ()
        {
            base.OnBackPressed();

            LevelLoader.Reload();
        }

        public void OnMainMenuPressed ()
        {
            LevelLoader.LoadLevel(mainMenuIndex);

            MainMenu.Open();
        }
    } 
}
