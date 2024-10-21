using Common;
using Extensions;
using UnityEngine.UIElements;

namespace Game
{
    public sealed class GameView
    {
        public VisualElement Container { get; private set; }
        public Button PauseButton { get; private set; }
        public Button HistoryButton { get; set; }

        public GameView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(CommonNames.GameViewName);
            Container = root;
            PauseButton = root.Q<Button>("pause-button");
            HistoryButton = root.Q<Button>("history-button");
            HistoryButton.Hide();
        }
    }
}
