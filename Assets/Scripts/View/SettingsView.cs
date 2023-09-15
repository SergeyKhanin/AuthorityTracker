using Common;
using Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace View
{
    public class SettingsView : MonoBehaviour
    {
        private VisualElement _root;
        private VisualElement _iconOpacityExample;
        private VisualElement _iconOpacitySliderRoot;
        private VisualElement _deckContainer;
        private VisualElement _diceContainer;
        private VisualElement _swapContainer;
        private VisualElement _toolsContainer;
        private CustomButton _backButton;
        private CustomButton _clearSettings;
        private Slider _iconOpacitySlider;
        private Toggle _deckToggle;
        private Toggle _diceToggle;
        private Toggle _swapToolsToggle;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _iconOpacityExample = _root.Q<VisualElement>("icon-opacity-example");
            _iconOpacitySliderRoot = _root.Q<VisualElement>("icon-opacity-slider");
            _toolsContainer = _root.Q<VisualElement>("tools-container");
            _backButton = _root.Q<CustomButton>("back-button");
            _clearSettings = _root.Q<CustomButton>("clear-settings-button");
            _deckContainer = _root.Q<VisualElement>("deck-toggle-container");
            _diceContainer = _root.Q<VisualElement>("dice-toggle-container");
            _swapContainer = _root.Q<VisualElement>("swap-toggle-container");
            _deckToggle = _deckContainer.Q<Toggle>("toggle");
            _diceToggle = _diceContainer.Q<Toggle>("toggle");
            _swapToolsToggle = _swapContainer.Q<Toggle>("toggle");
            _iconOpacitySlider = _iconOpacitySliderRoot.Q<Slider>();

            _iconOpacitySlider.RegisterValueChangedCallback(OnIconOpacitySliderChanged);
            _diceToggle.RegisterValueChangedCallback(SaveDiceVisibilityState);
            _deckToggle.RegisterValueChangedCallback(SaveDeckVisibilityState);
            _swapToolsToggle.RegisterValueChangedCallback(SaveToolsDirectionState);
        }

        private void Start()
        {
            SetPointsIconsOpacityValue();
            SetIconsOpacityStyle();
            SetToggleState(_diceToggle, CommonSaveParameters.DiceVisibility, CommonSaveParameters.DiceIsVisible);
            SetToggleState(_deckToggle, CommonSaveParameters.DeckVisibility, CommonSaveParameters.DeckIsVisible);
            SetToggleState(_swapToolsToggle, CommonSaveParameters.ToolsDirectionState, CommonSaveParameters.ToolsDirectionNormal);
        }

        private void SetIconsOpacityStyle() => _iconOpacityExample.style.opacity = _iconOpacitySlider.value;

        private void SetPointsIconsOpacityValue()
        {
            if (PlayerPrefs.HasKey(CommonSaveParameters.PointsIconsOpacity))
                _iconOpacitySlider.value = PlayerPrefs.GetFloat(CommonSaveParameters.PointsIconsOpacity);
            else
                _iconOpacitySlider.value = 0.1f;
        }

        private void OnEnable()
        {
            _backButton.clicked += OnBackButtonClicked;
            _clearSettings.clicked += OnClearSettingsButtonButtonClicked;
        }

        private void OnDisable()
        {
            _backButton.clicked -= OnBackButtonClicked;
            _clearSettings.clicked -= OnClearSettingsButtonButtonClicked;
        }

        private void OnBackButtonClicked() => SceneManager.LoadScene(CommonScenesList.MainMenuScene);

        private void OnClearSettingsButtonButtonClicked()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(CommonScenesList.MainMenuScene);
        }

        private void OnIconOpacitySliderChanged(ChangeEvent<float> evt)
        {
            SavePointsIconsOpacityValue(evt.newValue);
            _iconOpacityExample.style.opacity = evt.newValue;
        }

        private void SaveDiceVisibilityState(ChangeEvent<bool> evt) => SaveState(evt.newValue,
            CommonSaveParameters.DiceVisibility,
            CommonSaveParameters.DiceIsVisible,
            CommonSaveParameters.DiceIsNotVisible);

        private void SaveDeckVisibilityState(ChangeEvent<bool> evt) => SaveState(evt.newValue,
            CommonSaveParameters.DeckVisibility,
            CommonSaveParameters.DeckIsVisible,
            CommonSaveParameters.DeckIsNotVisible);

        private void SaveToolsDirectionState(ChangeEvent<bool> evt)
        {
            SaveState(evt.newValue,
                CommonSaveParameters.ToolsDirectionState,
                CommonSaveParameters.ToolsDirectionNormal,
                CommonSaveParameters.ToolsDirectionReverse);

            _toolsContainer.EnableInClassList(CommonUssClassNames.ToolsSwapRow, evt.newValue);
        }

        private void SetToggleState(Toggle toggle, string keyName, string keyState)
        {
            if (PlayerPrefs.HasKey(keyName))
            {
                var stringName = PlayerPrefs.GetString(keyName);
                var isVisible = stringName == keyState;

                toggle.value = isVisible;
            }
            else
                toggle.value = false;
        }

        private static void SavePointsIconsOpacityValue(float value)
        {
            PlayerPrefs.SetFloat(CommonSaveParameters.PointsIconsOpacity, value);
            PlayerPrefs.Save();
        }


        private void SaveState(bool state, string keyName, string keyStateTrue, string keyStateFalse)
        {
            if (state)
                PlayerPrefs.SetString(keyName, keyStateTrue);
            else
                PlayerPrefs.SetString(keyName, keyStateFalse);

            PlayerPrefs.Save();
        }
    }
}