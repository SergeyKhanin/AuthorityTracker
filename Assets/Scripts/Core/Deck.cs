using Common;
using UnityEngine;

namespace Core
{
    public class Deck
    {
        private readonly int _defaultValue = 1;
        private readonly int _limit = 99;
        public int DeckAmount { get; private set; }

        public Deck()
        {
            if (PlayerPrefs.HasKey(CommonSaveParameters.DeckAmount))
                DeckAmount = PlayerPrefs.GetInt(CommonSaveParameters.DeckAmount);
            else
            {
                PlayerPrefs.SetInt(CommonSaveParameters.DeckAmount, _defaultValue);
                DeckAmount = _defaultValue;
            }
        }

        public void PlusDeckAmount()
        {
            DeckAmount++;
            ValidateDeckAmount();
        }

        public void MinusDeckAmount()
        {
            DeckAmount--;
            ValidateDeckAmount();
        }

        private void ValidateDeckAmount()
        {
            if (DeckAmount >= _limit)
                DeckAmount = _limit;
            if (DeckAmount <= 0)
                DeckAmount = 0;
        }
    }
}