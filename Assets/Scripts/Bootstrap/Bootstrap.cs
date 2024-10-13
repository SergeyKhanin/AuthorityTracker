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

            CreatePlayers(PlayersAmount.Player2);
        }

        private void CreatePlayers(PlayersAmount playersAmount)
        {
            for (int i = 1; i <= (int)playersAmount; i++)
            {
                var player = new PlayerEntity(_uiDocument, PlayerName + i);
            }
        }
    }
}
