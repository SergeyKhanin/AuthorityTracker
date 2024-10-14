using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class GameManager : MonoBehaviour
    {
        private const string PlayerTemplatePath = "PlayerView";
        private const string PlayerNamePrefix = "player-";
        private UIDocument _uiDocument;

        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();

            var players = CreatePlayers(PlayersAmount.Player2);
        }

        private List<PlayerPresenter> CreatePlayers(PlayersAmount playersAmount)
        {
            var uiDocument = _uiDocument;
            var root = uiDocument.rootVisualElement;
            var players = new List<PlayerPresenter>();
            var playerTemplate = Resources.Load<VisualTreeAsset>(PlayerTemplatePath);

            for (int i = 1; i <= (int)playersAmount; i++)
            {
                var playerName = PlayerNamePrefix + i;
                var playerElement = playerTemplate.Instantiate();

                playerElement.name = playerName;
                root.Add(playerElement);

                var player = new PlayerPresenter(
                    new PlayerView(uiDocument, playerName),
                    new PlayerModel()
                );

                players.Add(player);
            }

            return players;
        }
    }
}
