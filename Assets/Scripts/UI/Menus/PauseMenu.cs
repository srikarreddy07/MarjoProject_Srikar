using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
        [SerializeField] int mainMenuIndex = 1;

        public void OnResumePressed ()
        {
            Time.timeScale = 1;
            base.OnBackPressed();
        }

        public void OnRestartPressed ()
        {
            Time.timeScale = 1;
            LevelLoader.Reload();

            base.OnBackPressed();
        }

        public void OnMainMenuPressed ()
        {
            Time.timeScale = 1;
            LevelLoader.LoadLevel(mainMenuIndex);

            MainMenu.Open();
        }

        public void OnQuitPressed ()
        {
            Application.Quit();
        }
    } 
}
