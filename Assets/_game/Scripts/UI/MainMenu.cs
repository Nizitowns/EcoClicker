using _game.Scripts.Audio;
using _game.Scripts.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace _game.Scripts.UI
{
    public class MainMenu : BaseUI
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private string gameName = "Game Title";

        private VisualElement _buttonContainer;
        private VisualElement _optionsContainer;
        
        private void Start()
        {
            InitMenu();
            ShowMainMenu();
        }
        
        private void InitMenu()
        {
            var titleContainer = container.CreateChild("title-container");   
            _buttonContainer = container.CreateChild("button-container");
            _optionsContainer = container.CreateChild("options-container");

            var title = titleContainer.CreateChild<Label>("title-text");
            title.text = gameName;

            var startGame = _buttonContainer.CreateChild<Button>("start-game-btn", "generic-button");
            startGame.text = "START";
            startGame.clickable.clicked += LoadScene;
            
            var options = _buttonContainer.CreateChild<Button>("options-btn", "generic-button");
            options.text = "OPTIONS";
            options.clickable.clicked += ShowOptions;
            
            var fullScreen = _buttonContainer.CreateChild<Button>("full-screen-btn");
            fullScreen.text = "FULLSCREEN";
            fullScreen.clickable.clicked += ToggleFullScreen;
            fullScreen.style.display = DisplayStyle.None;
            
            var volumeSlider = _optionsContainer.CreateChild<Slider>("volume-slider");
            volumeSlider.lowValue = 0;
            volumeSlider.highValue = 1;
            volumeSlider.value = 0.5f;
            volumeSlider.RegisterValueChangedCallback(ChangeVolume);
            
            var optionsTest = _optionsContainer.CreateChild<Button>("options-btn", "generic-button");
            optionsTest.text = "Back";
            optionsTest.clickable.clicked += ShowMainMenu;
            
        }

        private void ChangeVolume(ChangeEvent<float> evt)
        {
            SoundManager.Instance.SetVolume(evt.newValue);
        }

        private void ShowMainMenu()
        {
            _buttonContainer.style.display = DisplayStyle.Flex;
            _optionsContainer.style.display = DisplayStyle.None;
        }

        private void ShowOptions()
        {
            _buttonContainer.style.display = DisplayStyle.None;
            _optionsContainer.style.display = DisplayStyle.Flex;
        }

        private void ToggleFullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
            //Debug.Log("Fullscreen: " + Screen.width + "x" + Screen.height);
            //Need to update panel settings to properly fit the new screen size
        }

        public void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

    }
}
