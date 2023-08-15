using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsView : MonoBehaviour
{
    private VisualElement _root;
    private CustomButton _backButton;
    private Slider _iconOpacitySlider;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _backButton = _root.Q<CustomButton>("back-button");
        _iconOpacitySlider = _root.Q<Slider>("icon-opacity-slider");

        _iconOpacitySlider.RegisterValueChangedCallback(OnIconOpacitySliderChanged);
    }

    private void OnEnable() => _backButton.clicked += OnBackButtonClicked;
    private void OnDisable() => _backButton.clicked -= OnBackButtonClicked;
    private void OnBackButtonClicked() => SceneManager.LoadScene(CommonScenesList.MainMenuScene);
    private void OnIconOpacitySliderChanged(ChangeEvent<float> evt) => SaveIconOpacity(evt.newValue);

    private static void SaveIconOpacity(float value)
    {
        PlayerPrefs.SetFloat("IconOpacity", value);
        PlayerPrefs.Save();
    }
}