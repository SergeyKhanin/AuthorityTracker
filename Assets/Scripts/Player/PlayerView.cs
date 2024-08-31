using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public sealed class PlayerView : MonoBehaviour
    {
        public Label PointsLabel { get; private set; }
        public Button X1PlusButton { get; private set; }
        public Button X5PlusButton { get; private set; }
        public Button X1MinusButton { get; private set; }
        public Button X5MinusButton { get; private set; }

        public void Init()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            PointsLabel = root.Q<Label>("points-label");
            X1PlusButton = root.Q<Button>("x1-plus-button");
            X5PlusButton = root.Q<Button>("x5-plus-button");
            X1MinusButton = root.Q<Button>("x1-minus-button");
            X5MinusButton = root.Q<Button>("x5-minus-button");
        }
    }
}
