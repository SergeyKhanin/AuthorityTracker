using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField]
        private UIDocument _uiDocument;

        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();

            var player1 = new PlayerPresenter(
                new PlayerView(_uiDocument, "player-1"),
                new PlayerModel()
            );
            player1.Init();

            var player2 = new PlayerPresenter(
                new PlayerView(_uiDocument, "player-2"),
                new PlayerModel()
            );
            player2.Init();
        }
    }
}
