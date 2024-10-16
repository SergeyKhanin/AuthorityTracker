using System.Collections.Generic;
using Common;
using Game;
using Menu;
using Player;
using Popup;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class GameBootstrap : MonoBehaviour
    {
        private PopupPresenter _popupPresenter;
        private GamePresenter _gamePresenter;
        private PausePresenter _pausePresenter;
        private readonly List<PlayerPresenter> _playerPresenters = new();

        private void Start() => CreateElements(GetUiDocument());

        private UIDocument GetUiDocument() => GetComponent<UIDocument>();

        private void CreateElements(UIDocument uiDocument)
        {
            _popupPresenter = new PopupPresenter(new PopupView(uiDocument));
            _gamePresenter = new GamePresenter(new GameView(uiDocument));
            _pausePresenter = new PausePresenter(new PauseView(uiDocument));

            CreatePlayers();
        }

        private void CreatePlayers()
        {
            var root = GetUiDocument().rootVisualElement;
            var playerTemplate = Resources.Load<VisualTreeAsset>(
                CommonTemplatePath.PlayerTemplatePath
            );
            var playersAmount = new SettingsModel().GetPlayersAmount();

            for (int i = 1; i <= playersAmount; i++)
            {
                var templateName = CommonNames.PlayerViewName + i;
                var playerName = CommonNames.PlayerName + i;
                var template = playerTemplate.Instantiate();

                template.name = templateName;
                root.Add(template);

                var playerPresenter = new PlayerPresenter(
                    new PlayerView(GetUiDocument(), templateName),
                    new PlayerModel(playerName)
                );

                _playerPresenters.Add(playerPresenter);
            }
        }

        private void OnDestroy()
        {
            _popupPresenter?.Dispose();
            _gamePresenter?.Dispose();
            _pausePresenter?.Dispose();

            foreach (var playerPresenter in _playerPresenters)
                playerPresenter?.Dispose();
        }
    }
}
