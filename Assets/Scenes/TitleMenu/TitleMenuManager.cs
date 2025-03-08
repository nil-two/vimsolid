using System;
using Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.TitleMenu
{
    public class TitleMenuManager : MonoBehaviour
    {
        public TextMeshProUGUI startMenuText;
        public TextMeshProUGUI resultMenuText;
        public TextMeshProUGUI configMenuText;
        public TextMeshProUGUI quitMenuText;
        public Color normalMenuColor;
        public Color focusedMenuColor;

        private LastTitleMenuSingleton _lastMenu;
        private TextMeshProUGUI[] _menuTexts;

        private void Start()
        {
            _lastMenu = LastTitleMenuSingleton.GetInstance();
            _menuTexts = new []{startMenuText, resultMenuText, configMenuText, quitMenuText};
            UpdateMenus();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.K))
            {
                FocusPrevMenu();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.J))
            {
                FocusNextMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                FocusQuitMenuOrSelectQuitMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Return) ||
                     (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M)))
            {
                SelectMenu();
            }
        }

        private void UpdateMenus()
        {
            foreach (var menuText in _menuTexts)
            {
                menuText.color = normalMenuColor;
                menuText.fontStyle = FontStyles.Normal;
            }
            _menuTexts[_lastMenu.Index].color = focusedMenuColor;
            _menuTexts[_lastMenu.Index].fontStyle = FontStyles.Underline;
        }

        private void FocusMenu(int menuIndex)
        {
            _lastMenu.Index = menuIndex;
            UpdateMenus();
        }

        private void FocusPrevMenu()
        {
            FocusMenu(_lastMenu.Index-1 >= 0 ? _lastMenu.Index-1 : _menuTexts.Length-1);
        }

        private void FocusNextMenu()
        {
            FocusMenu(_lastMenu.Index+1 <= _menuTexts.Length-1 ? _lastMenu.Index+1 : 0);
        }

        private void FocusQuitMenuOrSelectQuitMenu()
        {
            var focusedMenuText = _menuTexts[_lastMenu.Index];
            if (focusedMenuText != quitMenuText)
            {
                FocusMenu(Array.IndexOf(_menuTexts, quitMenuText));
                return;
            }
            SelectMenu();
        }

        private void SelectMenu()
        {
            var focusedMenuText = _menuTexts[_lastMenu.Index];
            if (focusedMenuText == startMenuText)
            {
                SceneManager.LoadScene("GameScene");
            }
            if (focusedMenuText == quitMenuText)
            {
                Quit();
            }
        }

        private static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
