using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    public class DiceView : MonoBehaviour
    {
        private VisualElement _root;
        private VisualElement _diceContainer;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _diceContainer = _root.Q<VisualElement>("dice-container");
        }
    }
}