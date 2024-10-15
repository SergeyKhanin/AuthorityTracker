using Common;
using UnityEngine.UIElements;

namespace Game
{
    public sealed class GameView
    {
        public VisualElement Container { get; private set; }
        public Button PauseButton { get; private set; }
        public Button BackButton { get; private set; }
        public Button MenuButton { get; private set; }
        public Button ResetButton { get; private set; }
        public Button QuitButton { get; private set; }

        public GameView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(CommonNames.GameViewName);
            Container = root;
            PauseButton = root.Q<Button>("pause-button");
            BackButton = root.Q<Button>("back-button");
            MenuButton = root.Q<Button>("menu-button");
            ResetButton = root.Q<Button>("reset-button");
            QuitButton = root.Q<Button>("quit-button");
        }
    }
}
