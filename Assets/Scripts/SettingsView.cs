using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsView : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _iconOpacityExample;
    private VisualElement _iconOpacitySliderRoot;
    private CustomButton _backButton;
    private Slider _iconOpacitySlider;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _iconOpacityExample = _root.Q<VisualElement>("icon-opacity-example");
        _iconOpacitySliderRoot = _root.Q<VisualElement>("icon-opacity-slider");
        _backButton = _root.Q<CustomButton>("back-button");
        _iconOpacitySlider = _iconOpacitySliderRoot.Q<Slider>();

        _iconOpacitySlider.RegisterValueChangedCallback(OnIconOpacitySliderChanged);
    }

    private void Start()
    {
        SetPointsIconsOpacityValue();
        SetIconsOpacityStyle();
    }

    private void SetIconsOpacityStyle() => _iconOpacityExample.style.opacity = _iconOpacitySlider.value;

    private void SetPointsIconsOpacityValue()
    {
        if (PlayerPrefs.HasKey("PointsIconsOpacity"))
            _iconOpacitySlider.value = PlayerPrefs.GetFloat("PointsIconsOpacity");
        else
            _iconOpacitySlider.value = 0.1f;
    }

    private void OnEnable() => _backButton.clicked += OnBackButtonClicked;
    private void OnDisable() => _backButton.clicked -= OnBackButtonClicked;
    private void OnBackButtonClicked() => SceneManager.LoadScene(CommonScenesList.MainMenuScene);

    private void OnIconOpacitySliderChanged(ChangeEvent<float> evt)
    {
        SavePointsIconsOpacityValue(evt.newValue);
        _iconOpacityExample.style.opacity = evt.newValue;
    }

    private static void SavePointsIconsOpacityValue(float value)
    {
        PlayerPrefs.SetFloat("PointsIconsOpacity", value);
        PlayerPrefs.Save();
    }
}