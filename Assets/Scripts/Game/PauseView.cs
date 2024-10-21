using Common;
using UnityEngine.UIElements;

namespace Game
{
    public sealed class PauseView
    {
        public VisualElement Container { get; private set; }
        public Button BackButton { get; private set; }
        public Button MenuButton { get; private set; }
        public Button RestartButton { get; private set; }
        public Button QuitButton { get; private set; }

        public PauseView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(CommonNames.PauseViewName);
            Container = root;
            BackButton = root.Q<Button>("back-button");
            MenuButton = root.Q<Button>("menu-button");
            RestartButton = root.Q<Button>("restart-button");
            QuitButton = root.Q<Button>("quit-button");
        }
    }
}
