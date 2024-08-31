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

        private VisualElement _root;

        public void Init()
        {
            Debug.Log("PlayerView init");
            _root = GetComponent<UIDocument>().rootVisualElement;
            PointsLabel = _root.Q<Label>("points-label");
            X1PlusButton = _root.Q<Button>("x1-plus-button");
            X5PlusButton = _root.Q<Button>("x5-plus-button");
            X1MinusButton = _root.Q<Button>("x1-minus-button");
            X5MinusButton = _root.Q<Button>("x5-minus-button");
        }
    }
}
