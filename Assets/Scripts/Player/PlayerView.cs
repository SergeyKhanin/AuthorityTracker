using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public sealed class PlayerView
    {
        private readonly UIDocument _uiDocument;
        private readonly string _pathToParent;
        public VisualElement Container { get; private set; }
        public Label PointsLabel { get; private set; }
        public Button X1PlusButton { get; private set; }
        public Button X5PlusButton { get; private set; }
        public Button X1MinusButton { get; private set; }
        public Button X5MinusButton { get; private set; }

        public PlayerView(UIDocument uiDocument, string pathToParent)
        {
            _uiDocument = uiDocument;
            _pathToParent = pathToParent;

            Init();
        }

        private void Init()
        {
            Container = _uiDocument.rootVisualElement.Q(_pathToParent);
            PointsLabel = Container.Q<Label>("points-label");
            X1PlusButton = Container.Q<Button>("x1-plus-button");
            X5PlusButton = Container.Q<Button>("x5-plus-button");
            X1MinusButton = Container.Q<Button>("x1-minus-button");
            X5MinusButton = Container.Q<Button>("x5-minus-button");
        }
    }
}
