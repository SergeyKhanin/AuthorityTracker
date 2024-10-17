using Common;
using UnityEngine;

namespace Player
{
    public sealed class PlayerModel
    {
        private const int StartPoints = 50;
        public int Points { get; private set; }
        public int Counter { get; private set; }

        public LifeVisualState PointsVisualState { get; private set; }

        public CounterVisualState CounterVisualState { get; private set; }

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

        public void X1Plus() => IncrementCounterPoints(1);

        public void X5Plus() => IncrementCounterPoints(5);

        public void X1Minus() => DecrementCounterPoints(1);

        public void X5Minus() => DecrementCounterPoints(5);

        public void Apply()
        {
            Points += Counter;

            if (_maxPoints <= Points)
                _maxPoints = Points;

            UpdateLifeVisualState();
            SavePlayerData();
            Clear();
        }

        public void RestartPoints()
        {
            Points = StartPoints;
            _maxPoints = StartPoints;

            SavePlayerData();
        }

        public void Clear()
        {
            Counter = 0;
            UpdateCounterVisualState();
        }

        private void IncrementCounterPoints(int points)
        {
            Counter += points;
            UpdateCounterVisualState();
        }

        private void DecrementCounterPoints(int points)
        {
            Counter -= points;
            UpdateCounterVisualState();
        }

        private void UpdateLifeVisualState()
        {
            if (Points <= 0)
                PointsVisualState = LifeVisualState.Zero;
            else if (Points < _maxPoints / 4)
                PointsVisualState = LifeVisualState.Quarter;
            else if (Points < _maxPoints / 2)
                PointsVisualState = LifeVisualState.Half;
            else
                PointsVisualState = LifeVisualState.Full;
        }

        private void UpdateCounterVisualState()
        {
            if (Counter == 0)
                CounterVisualState = CounterVisualState.Zero;
            else if (Counter < 0)
                CounterVisualState = CounterVisualState.Negative;
            else
                CounterVisualState = CounterVisualState.Positive;
        }

        private void SavePlayerData()
        {
            PlayerPrefs.SetInt(_playerName, Points);
            PlayerPrefs.SetInt(CommonNames.MaxPointsName + _playerName, _maxPoints);
            PlayerPrefs.Save();
        }
    }
}
