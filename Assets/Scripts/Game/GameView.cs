﻿using Common;
using UnityEngine.UIElements;

namespace Game
{
    public sealed class GameView
    {
        public VisualElement Container { get; private set; }
        public Button PauseButton { get; private set; }
        public Button HistoryOpenButton { get; set; }
        public Button HistoryCloseButton { get; set; }

        public GameView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(CommonNames.GameViewName);
            Container = root;
            PauseButton = root.Q<Button>("pause-button");
            HistoryOpenButton = root.Q<Button>("history-open-button");
            HistoryCloseButton = root.Q<Button>("history-close-button");
        }
    }
}
