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
        private CustomButton _backButton;
        private CustomButton _clearSettings;
        private Slider _iconOpacitySlider;
        private Toggle _deckToggle;
        private Toggle _diceToggle;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _iconOpacityExample = _root.Q<VisualElement>("icon-opacity-example");
            _iconOpacitySliderRoot = _root.Q<VisualElement>("icon-opacity-slider");
            _backButton = _root.Q<CustomButton>("back-button");
            _clearSettings = _root.Q<CustomButton>("clear-settings-button");
            _deckToggle = _root.Q<Toggle>("deck-toggle");
            _diceToggle = _root.Q<Toggle>("dice-toggle");
            _iconOpacitySlider = _iconOpacitySliderRoot.Q<Slider>();

            _iconOpacitySlider.RegisterValueChangedCallback(OnIconOpacitySliderChanged);
            _diceToggle.RegisterValueChangedCallback(SaveDiceVisibilityState);
            _deckToggle.RegisterValueChangedCallback(SaveDeckVisibilityState);
        }

        private void Start()
        {
            SetPointsIconsOpacityValue();
            SetIconsOpacityStyle();
            SetToggleVisibility(_diceToggle, CommonSaveParameters.DiceVisibility, CommonSaveParameters.DiceIsVisible);
            SetToggleVisibility(_deckToggle, CommonSaveParameters.DeckVisibility, CommonSaveParameters.DeckIsVisible);
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

        private void SaveDiceVisibilityState(ChangeEvent<bool> evt)
        {
            var isVisible = evt.newValue;

            if (isVisible)
                PlayerPrefs.SetString(CommonSaveParameters.DiceVisibility, CommonSaveParameters.DiceIsVisible);
            else
                PlayerPrefs.SetString(CommonSaveParameters.DiceVisibility, CommonSaveParameters.DiceIsNotVisible);

            PlayerPrefs.Save();
        }

        private void SaveDeckVisibilityState(ChangeEvent<bool> evt)
        {
            var isVisible = evt.newValue;

            if (isVisible)
                PlayerPrefs.SetString(CommonSaveParameters.DeckVisibility, CommonSaveParameters.DeckIsVisible);
            else
                PlayerPrefs.SetString(CommonSaveParameters.DeckVisibility, CommonSaveParameters.DeckIsNotVisible);

            PlayerPrefs.Save();
        }

        private void SetToggleVisibility(Toggle toggle, string keyName, string keyState)
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
    }
}