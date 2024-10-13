using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class Bootstrap : MonoBehaviour
    {
        private const string PlayerName = "player-";
        private UIDocument _uiDocument;

        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();

            var players = CreatePlayers(PlayersAmount.Player2);

            foreach (var player in players)
            {
                player.Apply();
            }
        }

        private List<PlayerPresenter> CreatePlayers(PlayersAmount playersAmount)
        {
            var players = new List<PlayerPresenter>();

            for (int i = 1; i <= (int)playersAmount; i++)
            {
                var player = new PlayerPresenter(
                    new PlayerView(_uiDocument, PlayerName + i),
                    new PlayerModel()
                );
                players.Add(player);
            }

            return players;
        }
    }
}
