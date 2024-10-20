using Common;
using UnityEngine;

namespace Player
{
    public sealed class PlayerModel
    {
        private const int StartPoints = 50;
        private const int CounterLimit = 99;
        private const int PointsLimitTopCap = 999;
        private const int PointsLimitBottomCap = -99;
        public int Points { get; private set; }
        public int Counter { get; private set; }
        public PointsVisualState PointsVisualState { get; private set; }
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

            UpdatePointsVisualState();
        }

        public void X1Plus() => IncrementCounterPoints(1);

        public void X5Plus() => IncrementCounterPoints(5);

        public void X1Minus() => DecrementCounterPoints(1);

        public void X5Minus() => DecrementCounterPoints(5);

        public void Apply()
        {
            Points += Counter;

            SetMaxPoints();
            ValidatePointsValue();
            UpdatePointsVisualState();
            SavePlayerData();
            Clear();
        }

        public void RestartPoints()
        {
            Points = StartPoints;
            _maxPoints = StartPoints;

            UpdatePointsVisualState();
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
            ValidateCounterValue();
            UpdateCounterVisualState();
        }

        private void DecrementCounterPoints(int points)
        {
            Counter -= points;
            ValidateCounterValue();
            UpdateCounterVisualState();
        }

        private void SetMaxPoints()
        {
            if (_maxPoints <= Points)
                _maxPoints = Points;
        }

        private void UpdatePointsVisualState()
        {
            if (Points <= 0)
                PointsVisualState = PointsVisualState.Zero;
            else if (Points < _maxPoints / 4)
                PointsVisualState = PointsVisualState.Quarter;
            else if (Points < _maxPoints / 2)
                PointsVisualState = PointsVisualState.Half;
            else
                PointsVisualState = PointsVisualState.Full;
        }

        private void UpdateCounterVisualState()
        {
            if (Counter < 0)
                CounterVisualState = CounterVisualState.Negative;
            else if (Counter == 0)
                CounterVisualState = CounterVisualState.Zero;
            else
                CounterVisualState = CounterVisualState.Positive;
        }

        private void ValidateCounterValue()
        {
            Counter = Counter switch
            {
                > CounterLimit => CounterLimit,
                < -CounterLimit => -CounterLimit,
                _ => Counter
            };
        }

        private void ValidatePointsValue()
        {
            Points = Points switch
            {
                > PointsLimitTopCap => PointsLimitTopCap,
                < PointsLimitBottomCap => PointsLimitBottomCap,
                _ => Points
            };
        }

        private void SavePlayerData()
        {
            PlayerPrefs.SetInt(_playerName, Points);
            PlayerPrefs.SetInt(CommonNames.MaxPointsName + _playerName, _maxPoints);
            PlayerPrefs.Save();
        }
    }
}
