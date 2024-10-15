using Common;
using UnityEngine.UIElements;

namespace Menu
{
    public sealed class MenuView
    {
        public VisualElement Container { get; private set; }
        public Button StartButton { get; private set; }
        public Button SettingsButton { get; private set; }
        public Button ContinueButton { get; private set; }
        public Button ResetButton { get; private set; }
        public Button QuitButton { get; private set; }

        public MenuView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(CommonNames.MenuViewName);
            Container = root;
            StartButton = root.Q<Button>("start-button");
            SettingsButton = root.Q<Button>("settings-button");
            ContinueButton = root.Q<Button>("continue-button");
            ResetButton = root.Q<Button>("reset-button");
            QuitButton = root.Q<Button>("quit-button");
        }
    }
}
