using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        //[SerializeField] DataManager dataManager;

        public void OnPlayPressed ()
        {
            LevelSelectionMenu.Open();
        }

        public void OnSettingPressed ()
        {
            SettingMenu.Open();
        }

        public void OnCreditsPressed ()
        {
            CreditsMenu.Open();
        }

        public void OnQuitPressed ()
        {
            Application.Quit();
        }
    }
} 
