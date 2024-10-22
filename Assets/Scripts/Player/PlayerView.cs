using UnityEngine.UIElements;

namespace Player
{
    public sealed class PlayerView
    {
        public VisualElement PlayerContainerHistory { get; set; }
        public VisualElement PlayerContainerControl { get; set; }
        public VisualElement CounterContainer { get; set; }
        public VisualElement PointsImage { get; set; }

        public Label HistoryLabel { get; private set; }
        public Label PointsLabel { get; private set; }
        public Label CounterLabel { get; private set; }
        public Button X1PlusButton { get; private set; }
        public Button X5PlusButton { get; private set; }
        public Button X1MinusButton { get; private set; }
        public Button X5MinusButton { get; private set; }

        public PlayerView(UIDocument uiDocument, string pathToParent)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(pathToParent);
            PlayerContainerControl = root.Q<VisualElement>("player-container-control");
            PlayerContainerHistory = root.Q<VisualElement>("player-container-history");
            PointsImage = root.Q<VisualElement>("points-image");
            CounterContainer = root.Q<VisualElement>("counter-container");
            HistoryLabel = root.Q<Label>("history-label");
            PointsLabel = root.Q<Label>("points-label");
            CounterLabel = root.Q<Label>("counter-label");
            X1PlusButton = root.Q<Button>("x1-plus-button");
            X5PlusButton = root.Q<Button>("x5-plus-button");
            X1MinusButton = root.Q<Button>("x1-minus-button");
            X5MinusButton = root.Q<Button>("x5-minus-button");
        }
    }
}
