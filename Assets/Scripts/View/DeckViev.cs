using Common;
using Core;
using Elements;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    public class DeckView : MonoBehaviour
    {
        private Deck _deck;
        private VisualElement _root;
        private VisualElement _deckContainer;
        private CustomLabel _deckLabel;
        private CustomButton _arrowLeftButton;
        private CustomButton _arrowRightButton;
        private int _deckCounter;

        private void Awake()
        {
            _deck = new Deck();
            _root = GetComponent<UIDocument>().rootVisualElement;

            _deckContainer = _root.Q<VisualElement>("deck-container");
            _deckLabel = _deckContainer.Q<CustomLabel>("deck-label");
            _arrowLeftButton = _deckContainer.Q<CustomButton>("arrow-left-button");
            _arrowRightButton = _deckContainer.Q<CustomButton>("arrow-right-button");

            _deckCounter = _deck.DeckAmount;
            _deckLabel.text = _deckCounter.ToString();
        }

        private void OnEnable()
        {
            _arrowLeftButton.clicked += OnArrowLeftButtonClicked;
            _arrowRightButton.clicked += OnArrowRightButtonClicked;
        }

        private void OnDisable()
        {
            _arrowLeftButton.clicked -= OnArrowLeftButtonClicked;
            _arrowRightButton.clicked -= OnArrowRightButtonClicked;
        }

        private void OnArrowLeftButtonClicked()
        {
            _deck.MinusDeckAmount();
            _deckLabel.text = _deck.DeckAmount.ToString();
            SaveDeckAmount(_deck.DeckAmount);
        }

        private void OnArrowRightButtonClicked()
        {
            _deck.PlusDeckAmount();
            _deckLabel.text = _deck.DeckAmount.ToString();
            SaveDeckAmount(_deck.DeckAmount);
        }
    
        private void SaveDeckAmount(int deckAmount)
        {
            PlayerPrefs.SetInt(CommonSaveParameters.DeckAmount, deckAmount);
            PlayerPrefs.Save();
        }
    }
}