using System;
using UnityEngine;

namespace _Project.Script.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuUI mainMenu;
        [SerializeField] private ImprovementsMenu improvementsMenu;

        private void Awake()
        {
            mainMenu.OnImprovementsClicked += ()=> SwitchMenu(false);
            improvementsMenu.OnBackClicked += () => SwitchMenu(true);
        }

        private void SwitchMenu(bool toMain)
        {
            mainMenu.gameObject.SetActive(toMain);
            improvementsMenu.gameObject.SetActive(!toMain);
        }
    }
}