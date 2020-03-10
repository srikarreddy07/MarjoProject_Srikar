using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

namespace LevelManagement
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Menus")]
        [SerializeField] MainMenu mainMenuPrefab;
        [SerializeField] SettingMenu settingMenuPrefab;
        [SerializeField] CreditsMenu creditsMenuPrefab;
        [SerializeField] PauseMenu pauseMenuPrefab;
        [SerializeField] GameMenu gameMenuPrefab;
        [SerializeField] LevelCompleted levelCompletedPrefab;
        [SerializeField] LevelSelectionMenu levelSelectionMenuPrefab;
        [SerializeField] Transform menuParent;

        private Stack<Menu> menuStack = new Stack<Menu>();

        private static MenuManager instance;
        public static MenuManager Instance { get => instance; }

        private void Awake()
        {
            if (instance != null)
                Destroy(this.gameObject);
            else
            {
                instance = this;
                InitiazeMenus();

                Object.DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

        private void InitiazeMenus ()
        {
            if(menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menu");
                menuParent = menuParentObject.transform;
            }
            Object.DontDestroyOnLoad(menuParent);

            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

            foreach (FieldInfo field in fields)
            {
                Menu prefab = field.GetValue(this) as Menu;

                if(prefab != null)
                {
                    Menu menuInstance = Instantiate(prefab, menuParent);

                    if (prefab != mainMenuPrefab)
                        menuInstance.gameObject.SetActive(false);
                    else
                        OpenMenu(menuInstance);
                }
            }
        }

        public void OpenMenu (Menu menuInstance)
        {
            if(menuInstance == null)
            {
                Debug.LogWarning("Load Level Deosn't exist");
                return;
            }
           
            foreach(Menu menu in menuStack)
            {
                menu.gameObject.SetActive(false);
            }

            menuInstance.gameObject.SetActive(true);
            menuStack.Push(menuInstance);
        }

        public void CloseMenu ()
        {
            if(menuStack.Count == 0)
            {
                Debug.LogWarning("No More Menu in the stock to close");
                return;
            }

            Menu topmenu = menuStack.Pop();
            topmenu.gameObject.SetActive(false);

            if(menuStack.Count > 0)
            {
                Menu nextMenu = menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }
    }
}
