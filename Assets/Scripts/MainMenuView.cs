using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuView : MonoBehaviour
{
    private Authority _authority;
    private int _pointsLimit;
    private bool _hasOnePlayer;
    private bool _hasTwoPlayers;

    private VisualElement _root;
    private IntegerField _initialPointsIntegerField;
    private TextElement _pointsTextElement;
    private CustomButton _casualButton;
    private CustomButton _tournamentButton;
    private CustomButton _communityButton;
    private CustomButton _quitButton;
    private CustomButton _onePlayerButton;
    private CustomButton _twoPlayersButton;
    private CustomButton _settingsButton;


    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _initialPointsIntegerField = _root.Q<IntegerField>("initial-points-input");
        _pointsTextElement = _root.Query<TextElement>();
        _casualButton = _root.Q<CustomButton>("casual-button");
        _tournamentButton = _root.Q<CustomButton>("tournament-button");
        _communityButton = _root.Q<CustomButton>("community-button");
        _quitButton = _root.Q<CustomButton>("quit-button");
        _onePlayerButton = _root.Q<CustomButton>("one-player-button");
        _twoPlayersButton = _root.Q<CustomButton>("two-players-button");
        _settingsButton = _root.Q<CustomButton>("settings-button");

        _authority = new Authority();
        _pointsLimit = _authority.Limit;
        _hasTwoPlayers = true;

        _onePlayerButton.EnableInClassList(CommonUssClassNames.Hide, true);
        _twoPlayersButton.EnableInClassList(CommonUssClassNames.Hide, false);

        _initialPointsIntegerField.RegisterCallback<ChangeEvent<int>>(OnIntChangedEvent);

        if (PlayerPrefs.HasKey("InitialPoints"))
            _initialPointsIntegerField.value = PlayerPrefs.GetInt("InitialPoints");
    }

    private void Start()
    {
        _root.RegisterCallback<GeometryChangedEvent>(SaveScreenResolutions);
    }

    private void OnEnable()
    {
        _onePlayerButton.clicked += OnOnePlayerButtonClicked;
        _twoPlayersButton.clicked += OnTwoPlayersButtonClicked;
        _settingsButton.clicked += OnSettingsButtonClicked;
        _casualButton.clicked += OnCasualButtonClicked;
        _tournamentButton.clicked += OnTournamentButtonClicked;
        _quitButton.clicked += OnQuitButtonClicked;
        _communityButton.clicked += OnCommunityButtonClicked;
    }

    private void OnDisable()
    {
        _onePlayerButton.clicked -= OnOnePlayerButtonClicked;
        _twoPlayersButton.clicked -= OnTwoPlayersButtonClicked;
        _settingsButton.clicked -= OnSettingsButtonClicked;
        _casualButton.clicked -= OnCasualButtonClicked;
        _tournamentButton.clicked -= OnTournamentButtonClicked;
        _communityButton.clicked -= OnCommunityButtonClicked;
        _quitButton.clicked -= OnQuitButtonClicked;
    }

    private void OnQuitButtonClicked() => Application.Quit();

    private void OnOnePlayerButtonClicked()
    {
        _hasOnePlayer = false;
        _hasTwoPlayers = true;
        _onePlayerButton.EnableInClassList(CommonUssClassNames.Hide, _hasTwoPlayers);
        _twoPlayersButton.EnableInClassList(CommonUssClassNames.Hide, _hasOnePlayer);
    }

    private void OnTwoPlayersButtonClicked()
    {
        _hasOnePlayer = true;
        _hasTwoPlayers = false;
        _twoPlayersButton.EnableInClassList(CommonUssClassNames.Hide, _hasOnePlayer);
        _onePlayerButton.EnableInClassList(CommonUssClassNames.Hide, _hasTwoPlayers);
    }

    private void OnSettingsButtonClicked() => SceneManager.LoadScene(CommonScenesList.SettingsScene);

    private void OnCasualButtonClicked()
    {
        if (_hasOnePlayer)
            SceneManager.LoadScene(CommonScenesList.Casual1PlayerGameScene);

        if (_hasTwoPlayers)
            SceneManager.LoadScene(CommonScenesList.Casual2PlayersGameScene);
    }

    private void OnTournamentButtonClicked()
    {
        if (_hasOnePlayer)
            SceneManager.LoadScene(CommonScenesList.Tournament1PlayerGameScene);

        if (_hasTwoPlayers)
            SceneManager.LoadScene(CommonScenesList.Tournament2PlayersGameScene);
    }

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

    private void SaveScreenResolutions(GeometryChangedEvent evt)
    {
        PlayerPrefs.SetFloat("ScreenResolutionsWidth", _root.resolvedStyle.width);
        PlayerPrefs.SetFloat("ScreenResolutionsHeight", _root.resolvedStyle.height);
        PlayerPrefs.Save();
    }

    private void OnCommunityButtonClicked()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                Application.OpenURL(CommonCommunitiesPages.En);
                break;
            case SystemLanguage.Russian:
                Application.OpenURL(CommonCommunitiesPages.Ru);
                break;
            default:
                Application.OpenURL(CommonCommunitiesPages.None);
                break;
        }
    }
}