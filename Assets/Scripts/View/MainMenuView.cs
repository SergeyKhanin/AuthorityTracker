using Common;
using Core;
using Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace View
{
    public class MainMenuView : MonoBehaviour
    {
        private Authority _authority;
        private bool _hasOnePlayer;
        private bool _hasTwoPlayers;

        private VisualElement _root;
        private VisualElement _playersContainer;
        private IntegerField _initialPointsIntegerField;
        private TextElement _pointsTextElement;
        private CustomButton _casualButton;
        private CustomButton _tournamentButton;
        private CustomButton _communityButton;
        private CustomButton _quitButton;
        private CustomButton _settingsButton;
        private Toggle _playersToggle;

        private void Awake()
        {
            _authority = new Authority();

            _root = GetComponent<UIDocument>().rootVisualElement;
            _initialPointsIntegerField = _root.Q<IntegerField>("initial-points-input");
            _playersContainer = _root.Q<VisualElement>("player-amount-toggle-container");
            _casualButton = _root.Q<CustomButton>("casual-button");
            _tournamentButton = _root.Q<CustomButton>("tournament-button");
            _communityButton = _root.Q<CustomButton>("community-button");
            _quitButton = _root.Q<CustomButton>("quit-button");
            _settingsButton = _root.Q<CustomButton>("settings-button");
            _playersToggle = _playersContainer.Q<Toggle>("toggle");
            _pointsTextElement = _initialPointsIntegerField.Q<TextElement>();
            
            GetInitialPoints();
            SetInitialPoints();
            SetPlayerState();
        }

        private void Start()
        {
            CheckFontSize(_initialPointsIntegerField.value);
            _playersToggle.RegisterValueChangedCallback(SavePlayersAmountState);
        }

        private void OnEnable()
        {
            _settingsButton.clicked += OnSettingsButtonClicked;
            _casualButton.clicked += OnCasualButtonClicked;
            _tournamentButton.clicked += OnTournamentButtonClicked;
            _quitButton.clicked += OnQuitButtonClicked;
            _communityButton.clicked += OnCommunityButtonClicked;
        }

        private void OnDisable()
        {
            _settingsButton.clicked -= OnSettingsButtonClicked;
            _casualButton.clicked -= OnCasualButtonClicked;
            _tournamentButton.clicked -= OnTournamentButtonClicked;
            _communityButton.clicked -= OnCommunityButtonClicked;
            _quitButton.clicked -= OnQuitButtonClicked;
        }

        private void OnQuitButtonClicked() => Application.Quit();


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

            if (value > 999 || value < 1)
                _initialPointsIntegerField.value = 1;
            else
                _initialPointsIntegerField.value = value;

            SaveInitialAuthorityPoints(value);
            CheckFontSize(value);
        }

        private void CheckFontSize(int value)
        {
            var isIntLonger = value > 99;
            _pointsTextElement.EnableInClassList(CommonUssClassNames.LabelAuthoritySizeSmall, isIntLonger);
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

        private void SaveAuthorityPoints(int value)
        {
            PlayerPrefs.SetInt(CommonSaveParameters.InitialPoints, value);
            PlayerPrefs.SetInt(CommonSaveParameters.Player1, value);
            PlayerPrefs.SetInt(CommonSaveParameters.Player1MaxPoints, value);
            PlayerPrefs.SetInt(CommonSaveParameters.Player2, value);
            PlayerPrefs.SetInt(CommonSaveParameters.Player2MaxPoints, value);
            PlayerPrefs.Save();
        }

        private void SaveInitialAuthorityPoints(int value) => SaveAuthorityPoints(value);
        private void SaveStartAuthorityPoints() => SaveAuthorityPoints(_authority.Points);
        private void GetInitialPoints() => _initialPointsIntegerField.value = _authority.Points;
        private void SetInitialPoints() => _initialPointsIntegerField.RegisterCallback<ChangeEvent<int>>(OnIntChangedEvent);

        private void SavePlayersAmountState(ChangeEvent<bool> evt)
        {
            var state = evt.newValue;

            if (state)
                PlayerPrefs.SetString(CommonSaveParameters.PlayersAmount, CommonSaveParameters.HasTwoPlayers);

            else
                PlayerPrefs.SetString(CommonSaveParameters.PlayersAmount, CommonSaveParameters.HasOnePlayer);

            PlayerPrefs.Save();

            _hasOnePlayer = !state;
            _hasTwoPlayers = state;

            SaveStartAuthorityPoints();
        }

        private void SetPlayerState()
        {
            if (PlayerPrefs.HasKey(CommonSaveParameters.PlayersAmount))
            {
                var stringName = PlayerPrefs.GetString(CommonSaveParameters.PlayersAmount);
                var state = stringName == CommonSaveParameters.HasOnePlayer;

                _playersToggle.value = !state;
                _hasOnePlayer = state;
                _hasTwoPlayers = !state;
            }
            else
            {
                _playersToggle.value = true;
                _hasTwoPlayers = true;
                _hasOnePlayer = false;
            }
        }
    }
}