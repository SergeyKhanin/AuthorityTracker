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

        public string History =>
            string.Join(
                Environment.NewLine,
                _history.Split(",").Take(_history.Split(",").Length - 1).Reverse()
            );

        public PointsImageVisualState PointsImageVisualState { get; private set; }
        public PointsLabelVisualState PointsLabelVisualState { get; private set; }
        public CounterVisualState CounterVisualState { get; private set; }
        private readonly string _playerName;
        private readonly StringBuilder _historyBuilder;
        private string _history;
        private int _maxPoints;

        public PlayerModel(string playerName)
        {
            _playerName = playerName;
            _historyBuilder = new StringBuilder();

            if (PlayerPrefs.HasKey(playerName))
            {
                Points = PlayerPrefs.GetInt(playerName);
                _maxPoints = PlayerPrefs.GetInt(CommonNames.MaxPointsName + playerName);
                _history = _historyBuilder
                    .Append(PlayerPrefs.GetString(CommonNames.HistoryName + playerName))
                    .ToString();
            }
            else
            {
                Points = StartPoints;
                _maxPoints = StartPoints;
                _history = _historyBuilder.Clear().ToString();
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
            ResetCounter();
        }

        public void Restart()
        {
            Points = StartPoints;
            _history = _historyBuilder.Clear().ToString();
            DeletePlayerData();
        }

        public void ResetCounter()
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

            var line = Counter switch
            {
                < 0
                    => $"{Points}<color=#D73733>({pointsCash}{sing}{Math.Abs(counterCash)})</color>,",
                0 => $"{Points},",
                _ => $"{Points}<color=#438B44>({pointsCash}{sing}{Math.Abs(counterCash)})</color>,"
            };

            _historyBuilder.Append(line);
            _history = _historyBuilder.ToString();
        }

        private void SavePlayerData()
        {
            PlayerPrefs.SetInt(_playerName, Points);
            PlayerPrefs.SetInt(CommonNames.MaxPointsName + _playerName, _maxPoints);
            PlayerPrefs.SetString(CommonNames.HistoryName + _playerName, _history);
            PlayerPrefs.Save();
        }

        private void DeletePlayerData()
        {
            if (PlayerPrefs.HasKey(_playerName))
            {
                PlayerPrefs.DeleteKey(_playerName);
                PlayerPrefs.DeleteKey(CommonNames.MaxPointsName + _playerName);
                PlayerPrefs.DeleteKey(CommonNames.HistoryName + _playerName);
            }
        }
    }
}
