using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameView : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _playersFrame;
    private VisualElement _settingsFrame;

    private CustomButton _settingsButton;
    private CustomButton _backButton;
    private CustomButton _quitButton;
    private CustomButton _resetButton;
    private CustomButton _mainMenuButton;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _playersFrame = _root.Q<VisualElement>("players-frame");
        _settingsFrame = _root.Q<VisualElement>("settings-frame");

        _settingsButton = _root.Q<CustomButton>("settings-button");
        _backButton = _root.Q<CustomButton>("back-button");
        _quitButton = _root.Q<CustomButton>("quit-button");
        _resetButton = _root.Q<CustomButton>("reset-button");
        _mainMenuButton = _root.Q<CustomButton>("main-menu-button");
    }

    private void OnEnable()
    {
        _settingsButton.clicked += OnSettingsButtonClicked;
        _backButton.clicked += OnBackButtonClicked;
        _resetButton.clicked += OnResetButtonClicked;
        _mainMenuButton.clicked += OnMainMenuButtonClicked;
        _quitButton.clicked += OnQuitAppButtonClicked;
    }

    private void OnDisable()
    {
        _settingsButton.clicked -= OnSettingsButtonClicked;
        _backButton.clicked -= OnBackButtonClicked;
        _resetButton.clicked -= OnResetButtonClicked;
        _mainMenuButton.clicked -= OnMainMenuButtonClicked;
        _quitButton.clicked -= OnQuitAppButtonClicked;
    }

    private void OnSettingsButtonClicked() => EnableSettings(true);

    private void OnBackButtonClicked() => EnableSettings(false);

    private void OnResetButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    private void OnMainMenuButtonClicked() => SceneManager.LoadScene(0);
    private void OnQuitAppButtonClicked() => Application.Quit();

    private void EnableSettings(bool isEnabled)
    {
        if (isEnabled)
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        else
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        _playersFrame.EnableInClassList(CommonUssClassNames.Hide, isEnabled);
        _settingsButton.EnableInClassList(CommonUssClassNames.Hide, isEnabled);
        _settingsFrame.EnableInClassList(CommonUssClassNames.Hide, !isEnabled);
    }
}