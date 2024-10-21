using Common;
using UnityEngine;

namespace Player
{
    public sealed class PlayerModel
    {
        private const int StartPoints = 50;
        private const int CounterLimit = 99;
        private const int PointsLimitTopCap = 999;
        private const int PointsLimitBottomCap = 99;
        public int Points { get; private set; }
        public int Counter { get; private set; }
        public PointsImageVisualState PointsImageVisualState { get; private set; }
        public PointsLabelVisualState PointsLabelVisualState { get; private set; }
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

            UpdatePointsImageVisualState();
            UpdatePointsLabelVisualState();
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
            UpdatePointsImageVisualState();
            UpdatePointsLabelVisualState();
            SavePlayerData();
            Clear();
        }

        public void RestartPoints()
        {
            Points = StartPoints;
            _maxPoints = StartPoints;

            UpdatePointsImageVisualState();
            UpdatePointsLabelVisualState();
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

        private void UpdatePointsImageVisualState()
        {
            if (Points <= 0)
                PointsImageVisualState = PointsImageVisualState.Zero;
            else if (Points < _maxPoints / 4)
                PointsImageVisualState = PointsImageVisualState.Quarter;
            else if (Points < _maxPoints / 2)
                PointsImageVisualState = PointsImageVisualState.Half;
            else
                PointsImageVisualState = PointsImageVisualState.Full;
        }

        private void UpdatePointsLabelVisualState()
        {
            PointsLabelVisualState = Points switch
            {
                < 0 or > PointsLimitBottomCap => PointsLabelVisualState.Small,
                _ => PointsLabelVisualState.Big
            };
        }

        private void UpdateCounterVisualState()
        {
            CounterVisualState = Counter switch
            {
                < 0 => CounterVisualState.Negative,
                0 => CounterVisualState.Zero,
                _ => CounterVisualState.Positive
            };
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
                < -PointsLimitBottomCap => -PointsLimitBottomCap,
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
