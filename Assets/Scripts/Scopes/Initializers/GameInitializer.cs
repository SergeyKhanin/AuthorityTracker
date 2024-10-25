using System;
using System.Collections.Generic;
using Common;
using Game;
using Menu;
using Player;
using Popup;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Scopes.Initializers
{
    public sealed class GameInitializer : IStartable, IDisposable
    {
        private PopupPresenter _popupPresenter;
        private GamePresenter _gamePresenter;
        private PausePresenter _pausePresenter;
        private readonly List<PlayerPresenter> _playerPresenters = new();
        private readonly UIDocument _uiDocument;

        [Inject]
        public GameInitializer(UIDocument uiDocument) => _uiDocument = uiDocument;

        public void Start() => CreateElements(_uiDocument);

        private void CreateElements(UIDocument uiDocument)
        {
            _popupPresenter = new PopupPresenter(new PopupView(uiDocument));
            _gamePresenter = new GamePresenter(new GameView(uiDocument));
            _pausePresenter = new PausePresenter(new PauseView(uiDocument));

            CreatePlayers();
            return;

            void CreatePlayers()
            {
                var root = uiDocument.rootVisualElement.Q<VisualElement>(
                    CommonNames.ContentContainer
                );
                var playerTemplate = Resources.Load<VisualTreeAsset>(
                    CommonTemplatePath.PlayerTemplatePath
                );
                var playersAmount = new SettingsModel().GetPlayersAmount();

                for (int i = 1; i <= playersAmount; i++)
                {
                    var templateName = CommonNames.PlayerViewName + i;
                    var playerName = CommonNames.PlayerName + i;
                    var playerClassName = CommonNames.PlayerName.ToLower() + "-" + i;
                    var template = playerTemplate.Instantiate();

                    template.name = templateName;
                    template.AddToClassList(playerClassName);
                    root.Add(template);

                    var playerPresenter = new PlayerPresenter(
                        new PlayerView(uiDocument, templateName),
                        new PlayerModel(playerName)
                    );

                    _playerPresenters.Add(playerPresenter);
                }
            }
        }

        public void Dispose()
        {
            _popupPresenter?.Dispose();
            _gamePresenter?.Dispose();
            _pausePresenter?.Dispose();

            foreach (var playerPresenter in _playerPresenters)
                playerPresenter?.Dispose();
        }
    }
}
