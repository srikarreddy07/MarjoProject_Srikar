using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        public void OnPlayPressed ()
        {
            //LevelSelectionMenu
        }

        public void OnSettingPressed ()
        {

        }

        public void OnCreditsPressed ()
        {

        }

        public void OnQuitPressed ()
        {
            Application.Quit();
        }
    }
} 
