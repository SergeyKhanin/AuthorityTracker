using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public sealed class PlayerView : MonoBehaviour
    {
        public Button X1PlusButton { get; private set; }
        public Button X5PlusButton { get; private set; }
        public Button X1MinusButton { get; private set; }
        public Button X5MinusButton { get; private set; }

        private VisualElement _root;

        public void Init()
        {
            X1PlusButton = _root.Q<Button>("x1-plus-button");
            X5PlusButton = _root.Q<Button>("x5-plus-button");
            X1MinusButton = _root.Q<Button>("x1-minus-button");
            X5MinusButton = _root.Q<Button>("x5-minus-button");
        }
    }
}
