using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuView : MonoBehaviour
{
    private Authority _authority;
    private int _pointsLimit;

    private VisualElement _root;
    private IntegerField _initialPointsIntegerField;
    private CustomButton _quitButton;
    private CustomButton _player1Button;
    private CustomButton _player2Button;
    private TextElement _pointsTextElement;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _initialPointsIntegerField = _root.Q<IntegerField>("initial-points-input");
        _pointsTextElement = _root.Query<TextElement>();
        _quitButton = _root.Q<CustomButton>("quit-button");
        _player1Button = _root.Q<CustomButton>("1-player-button");
        _player2Button = _root.Q<CustomButton>("2-players-button");

        _authority = new Authority();
        _pointsLimit = _authority.Limit;

        _initialPointsIntegerField.RegisterCallback<ChangeEvent<int>>(OnIntChangedEvent);

        if (PlayerPrefs.HasKey("InitialPoints"))
            _initialPointsIntegerField.value = PlayerPrefs.GetInt("InitialPoints");
    }

    private void OnEnable()
    {
        _quitButton.clicked += OnQuitButtonClicked;
        _player1Button.clicked += OnPlayer1ButtonClicked;
        _player2Button.clicked += OnPlayer2ButtonClicked;
    }

    private void OnDisable()
    {
        _quitButton.clicked -= OnQuitButtonClicked;
        _player1Button.clicked -= OnPlayer1ButtonClicked;
        _player2Button.clicked -= OnPlayer2ButtonClicked;
    }

    private void OnQuitButtonClicked() => Application.Quit();
    private void OnPlayer1ButtonClicked() => SceneManager.LoadScene(1);
    private void OnPlayer2ButtonClicked() => SceneManager.LoadScene(2);

    private void OnIntChangedEvent(ChangeEvent<int> evt)
    {
        var value = evt.newValue;

        if (value > _pointsLimit || value < 1)
            _initialPointsIntegerField.value = 1;
        else
            _initialPointsIntegerField.value = value;

        SaveInitialPoints(value);
        CheckFontSize(value);
    }

    private void CheckFontSize(int value)
    {
        var isIntLonger = value > 99;
        _pointsTextElement.EnableInClassList(CommonUssClassNames.LabelAuthoritySizeSmall, isIntLonger);
    }

    private void SaveInitialPoints(int value)
    {
        PlayerPrefs.SetInt("InitialPoints", value);
        PlayerPrefs.Save();
    }
}