using Common;
using Elements;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

namespace View
{
    public class DiceView : MonoBehaviour
    {
        private int _previousIndex = 6;

        private VisualElement _root;
        private VisualElement _diceContainer;
        private VisualElement _diceImage;
        private CustomButton _rollDieButton;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _diceContainer = _root.Q<VisualElement>("dice-container");
            _diceImage = _diceContainer.Q<VisualElement>("die-image");
            _rollDieButton = _diceContainer.Q<CustomButton>("roll-die-button");

            RandomDieImage();
        }

        private void OnEnable() => _rollDieButton.clicked += OnRollDieButtonClicked;
        private void OnDisable() => _rollDieButton.clicked -= OnRollDieButtonClicked;
        private void OnRollDieButtonClicked() => RandomDieImage();

        private void RandomDieImage()
        {
            var random = new Random();
            var index = random.Next(1, 7);
            _diceImage.RemoveFromClassList(CommonUssClassNames.DieImage + _previousIndex);
            _diceImage.AddToClassList(CommonUssClassNames.DieImage + index);
            _previousIndex = index;
        }
    }
}