using Common;
using UnityEngine;

namespace Player
{
    public sealed class PlayerModel
    {
        private const int StartPoints = 50;
        public int Points { get; private set; }
        public int Counter { get; private set; }

        private readonly string _playerName;
        private int _maxPoints;

        public PlayerModel(string playerName)
        {
            _playerName = playerName;

            if (PlayerPrefs.HasKey(playerName))
            {
                Points = PlayerPrefs.GetInt(playerName);
                _maxPoints = PlayerPrefs.GetInt(CommonNames.MaxPointsName + playerName);
            }
            else
            {
                Points = StartPoints;
                _maxPoints = StartPoints;
                SavePlayerData();
            }
        }

        public void X1Plus() => Counter += 1;

        public void X5Plus() => Counter += 5;

        public void X1Minus() => Counter -= 1;

        public void X5Minus() => Counter -= 5;

        public void Apply()
        {
            Points += Counter;
            Counter = 0;

            if (_maxPoints <= Points)
                _maxPoints = Points;

            SavePlayerData();
        }

        public void RestartPoints()
        {
            Points = StartPoints;
            _maxPoints = StartPoints;

            SavePlayerData();
        }

        public LifeVisualState GetLifeVisualState()
        {
            return Points switch
            {
                var x when x <= 0 => LifeVisualState.Zero,
                var x when x <= _maxPoints / 4 => LifeVisualState.Quarter,
                var x when x <= _maxPoints / 2 => LifeVisualState.Half,
                _ => LifeVisualState.None
            };
        }

        public void Clear() => Counter = 0;

        private void SavePlayerData()
        {
            PlayerPrefs.SetInt(_playerName, Points);
            PlayerPrefs.SetInt(CommonNames.MaxPointsName + _playerName, _maxPoints);
            PlayerPrefs.Save();
        }
    }
}
