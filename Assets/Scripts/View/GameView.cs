using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = System.Random;

public class GameView : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _playersFrame;
    private VisualElement _settingsFrame;

    private CustomButton _pauseButton;
    private CustomButton _backButton;
    private CustomButton _quitButton;
    private CustomButton _resetButton;
    private CustomButton _mainMenuButton;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _playersFrame = _root.Q<VisualElement>("players-frame");
        _settingsFrame = _root.Q<VisualElement>("pause-frame");

        _pauseButton = _root.Q<CustomButton>("pause-button");
        _backButton = _root.Q<CustomButton>("back-button");
        _quitButton = _root.Q<CustomButton>("quit-button");
        _resetButton = _root.Q<CustomButton>("reset-button");
        _mainMenuButton = _root.Q<CustomButton>("main-menu-button");
    }

    private void Start() => SetBackgroundImage();

    private void OnEnable()
    {
        _pauseButton.clicked += OnPauseButtonClicked;
        _backButton.clicked += OnBackButtonClicked;
        _resetButton.clicked += OnResetButtonClicked;
        _mainMenuButton.clicked += OnMainMenuButtonClicked;
        _quitButton.clicked += OnQuitAppButtonClicked;
    }

    private void OnDisable()
    {
        _pauseButton.clicked -= OnPauseButtonClicked;
        _backButton.clicked -= OnBackButtonClicked;
        _resetButton.clicked -= OnResetButtonClicked;
        _mainMenuButton.clicked -= OnMainMenuButtonClicked;
        _quitButton.clicked -= OnQuitAppButtonClicked;
    }

    private void OnPauseButtonClicked() => EnablePause(true);
    private void OnBackButtonClicked() => EnablePause(false);
    private void OnMainMenuButtonClicked() => SceneManager.LoadScene(CommonScenesList.MainMenuScene);
    private void OnQuitAppButtonClicked() => Application.Quit();

    private void OnResetButtonClicked()
    {
        var initialPoints = PlayerPrefs.GetInt(CommonSaveParameters.InitialPoints);

        PlayerPrefs.SetInt(CommonSaveParameters.Player1, initialPoints);
        PlayerPrefs.SetInt(CommonSaveParameters.Player1MaxPoints, initialPoints);
        PlayerPrefs.SetInt(CommonSaveParameters.Player2, initialPoints);
        PlayerPrefs.SetInt(CommonSaveParameters.Player2MaxPoints, initialPoints);
        PlayerPrefs.Save();
        PlayerPrefs.DeleteKey(CommonSaveParameters.DeckAmount);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void EnablePause(bool isEnabled)
    {
        if (isEnabled)
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        else
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        _playersFrame.EnableInClassList(CommonUssClassNames.Hide, isEnabled);
        _pauseButton.EnableInClassList(CommonUssClassNames.Hide, isEnabled);
        _settingsFrame.EnableInClassList(CommonUssClassNames.Hide, !isEnabled);
    }

    private void SetBackgroundImage()
    {
        var random = new Random();
        var index = random.Next(0, 4);

        switch (index)
        {
            case 1:
                _playersFrame.AddToClassList(CommonUssClassNames.FrameGameImage1);
                break;
            case 2:
                _playersFrame.AddToClassList(CommonUssClassNames.FrameGameImage2);
                break;
            case 3:
                _playersFrame.AddToClassList(CommonUssClassNames.FrameGameImage3);
                break;
            default:
                _playersFrame.AddToClassList(CommonUssClassNames.FrameGameImage);
                break;
        }
    }
}