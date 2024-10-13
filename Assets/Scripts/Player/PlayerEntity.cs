using UnityEngine.UIElements;

namespace Player
{
    public sealed class PlayerEntity
    {
        public PlayerEntity(UIDocument uiDocument, string pathToParent)
        {
            var playerPresenter = new PlayerPresenter(
                new PlayerView(uiDocument, pathToParent),
                new PlayerModel()
            );
        }
    }
}
