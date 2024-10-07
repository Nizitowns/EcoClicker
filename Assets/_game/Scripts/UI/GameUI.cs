using System;
using _game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace _game.Scripts.UI
{
    public class GameUI : BaseUI
    {
        
        [SerializeField] private string energyBarText = "2/10";
        [Space(20)]
        [SerializeField] private string sceneToLoad = "GameScene";

        private VisualElement _buttonContainer;
        private VisualElement _optionsContainer;
        
        private void Start()
        {
            InitGameUI();
        }

        private void InitGameUI()
        {
            var leftSideMenu = container.CreateChild("left-side-menu", "section");
            var middleMenu = container.CreateChild("middle-section", "section");
            var rightSideMenu = container.CreateChild("right-side-menu", "section");
            
            
            //Left Side Menu
            var leftSideButtons = leftSideMenu.CreateChild("left-side-buttons-container");
            var menuButton = CreateLeftButton(leftSideButtons, "Menu");
            menuButton.clickable.clicked += MenuButtonClicked;
            var seedButton = CreateLeftButton(leftSideButtons, "Seed");
            seedButton.clickable.clicked += SeedButtonClicked;
            var fertilizerButton = CreateLeftButton(leftSideButtons, "Fertilizer");
            fertilizerButton.clickable.clicked += FertilizerButtonClicked;
            //TODO: Placeholder
            var placeholderButton = CreateLeftButton(leftSideButtons, "Game", "Game");
            placeholderButton.clickable.clicked += () => SceneManager.LoadScene(sceneToLoad);
            
            //Middle Energy bar
            var upgradeButton = middleMenu.CreateChild<Button>("upgrade-btn", "upgrade-button");
            upgradeButton.text = "Upgrade Button";
            upgradeButton.clickable.clicked += UpgradeButtonClicked;
            var energyBar = middleMenu.CreateChild("energy-bar");
            var energyBarText = energyBar.CreateChild<Label>("energy-bar-text");
            energyBarText.text = this.energyBarText;
            var energyBarLabel = middleMenu.CreateChild<Label>("energy-bar-label");
            energyBarLabel.text = "Energy Bar";

            //Right Side Menu
            var waterTank = rightSideMenu.CreateChild("water-tank");
            var waterTankLabel = waterTank.CreateChild<Label>("water-tank-label");
            waterTankLabel.text = "Water Tank";
        }

        private void UpgradeButtonClicked()
        {
            Debug.Log("Upgrade Button Clicked");
        }

        private void FertilizerButtonClicked()
        {
            Debug.Log("Fertilizer Button Clicked");
        }

        private void SeedButtonClicked()
        {
            Debug.Log("Seed Button Clicked");
        }

        private void MenuButtonClicked()
        {
            Debug.Log("Menu Button Clicked");
        }

        private Button CreateLeftButton(VisualElement buttonParent, string labelText, string buttonText = "")
        {
            var button = buttonParent.CreateChild<Button>("left-side-btn", "rounded-square");
            button.text = buttonText;
            var label = buttonParent.CreateChild<Label>("left-side-btn-label", "menu-label");
            label.text = labelText;
            return button;
        }

        //TODO: implement validation in base UI
        /*private void OnValidate()
        {
            container.Clear();
            InitGameUI();
        }*/
    }
}
