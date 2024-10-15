using UnityEngine.UIElements;

namespace Menu
{
    public sealed class MenuView
    {
        public Button StartButton { get; private set; }
        public Button ContinueButton { get; private set; }
        public Button SettingsButton { get; private set; }
        public Button CommunityButton { get; private set; }
        public Button QuitButton { get; private set; }

        public MenuView(UIDocument uiDocument, string pathToParent)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(pathToParent);
            StartButton = root.Q<Button>("start-button");
            ContinueButton = root.Q<Button>("continue-button");
            SettingsButton = root.Q<Button>("settings-button");
            CommunityButton = root.Q<Button>("community-button");
            QuitButton = root.Q<Button>("quit-button");
        }
    }
}
