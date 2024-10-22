using System;
using System.Linq;
using System.Text;
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

        public string History
        {
            get
            {
                var splitHistory = _history.Split(Environment.NewLine);
                return string.Join(
                    Environment.NewLine,
                    splitHistory.Where(line => !string.IsNullOrWhiteSpace(line)).Reverse()
                );
            }
            private set => _history = value;
        }

        public PointsImageVisualState PointsImageVisualState { get; private set; }
        public PointsLabelVisualState PointsLabelVisualState { get; private set; }
        public CounterVisualState CounterVisualState { get; private set; }
        private readonly string _playerName;
        private readonly StringBuilder _historyBuilder;
        private int _maxPoints;
        private string _history;

        public PlayerModel(string playerName)
        {
            _playerName = playerName;
            _historyBuilder = new StringBuilder();

            if (PlayerPrefs.HasKey(playerName))
            {
                Points = PlayerPrefs.GetInt(playerName);
                _maxPoints = PlayerPrefs.GetInt(CommonNames.MaxPointsName + playerName);
                History = _historyBuilder
                    .Append(PlayerPrefs.GetString(CommonNames.HistoryName + playerName))
                    .ToString();
            }
            else
            {
                Points = StartPoints;
                _maxPoints = StartPoints;
                History = _historyBuilder.Clear().ToString();
            }

            UpdatePointsImageVisualState();
            UpdatePointsLabelVisualState();
        }

        public void X1Plus() => IncrementCounterPoints(1);

        public void X5Plus() => IncrementCounterPoints(5);

        public void X1Minus() => DecrementCounterPoints(-1);

        public void X5Minus() => DecrementCounterPoints(-5);

        public void Apply()
        {
            var pointsCash = Points;
            var counterCash = Counter;

            Points += Counter;

            SetMaxPoints();
            ValidatePointsValue();
            WriteHistory(pointsCash, counterCash);
            UpdatePointsImageVisualState();
            UpdatePointsLabelVisualState();
            SavePlayerData();
            Clear();
        }

        public void RestartPoints()
        {
            Points = StartPoints;
            _maxPoints = StartPoints;
            History = _historyBuilder.Clear().ToString();

            UpdatePointsImageVisualState();
            UpdatePointsLabelVisualState();
        }

        public void Clear()
        {
            Counter = 0;
            UpdateCounterVisualState();
        }

        private void IncrementCounterPoints(int points) => CalculateCounterValue(points);

        private void DecrementCounterPoints(int points) => CalculateCounterValue(points);

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

        private void CalculateCounterValue(int points)
        {
            Counter = Mathf.Clamp(Counter + points, -CounterLimit, CounterLimit);
            UpdateCounterVisualState();
        }

        private void ValidatePointsValue() =>
            Points = Mathf.Clamp(Points, -PointsLimitBottomCap, PointsLimitTopCap);

        private void WriteHistory(int pointsCash, int counterCash)
        {
            var sing = Counter < 0 ? "-" : "+";
            var line = $"{pointsCash}{sing}{Math.Abs(counterCash)}={Points}{Environment.NewLine}";

            _historyBuilder.Append(line);
            History = _historyBuilder.ToString();
        }

        private void SavePlayerData()
        {
            PlayerPrefs.SetInt(_playerName, Points);
            PlayerPrefs.SetInt(CommonNames.MaxPointsName + _playerName, _maxPoints);
            PlayerPrefs.SetString(CommonNames.HistoryName + _playerName, History);
            PlayerPrefs.Save();
        }
    }
}
