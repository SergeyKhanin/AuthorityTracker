using System;
using Events;
using UnityEngine.UIElements;

namespace Player
{
    public sealed class PlayerPresenter : IDisposable
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _model;

        public PlayerPresenter(PlayerView view, PlayerModel model)
        {
            _view = view;
            _model = model;

            Subscribe();
            SubscribeToEvents();
            HideCounterLabel();
            UpdatePointsLabel();
        }

        public void SetName(string name) => _model.SetName(name);

        public void SetPoints(int points) => _model.SetPoints(points);

        private void OnX1PlusButtonClicked() => PlusX1();

        private void OnX5PlusButtonClicked() => PlusX5();

        private void OnX1MinusButtonClicked() => MinusX1();

        private void OnX5MinusButtonClicked() => MinusX5();

        private void PlusX1()
        {
            _model.X1Plus();
            UpdateCounterLabel();
        }

        private void PlusX5()
        {
            _model.X5Plus();
            UpdateCounterLabel();
        }

        private void MinusX5()
        {
            _model.X5Minus();
            UpdateCounterLabel();
        }

        private void MinusX1()
        {
            _model.X1Minus();
            UpdateCounterLabel();
        }

        public void UpdatePointsLabel()
        {
            _view.PointsLabel.text = _model.Points.ToString();
        }

        private void UpdateCounterLabel()
        {
            EventsManager.CounterChanged.Invoke();
            _view.CounterLabel.text = _model.Counter.ToString();
            ShowCounterLabel();
        }

        private void Apply()
        {
            _model.Apply();
            UpdatePointsLabel();
            HideCounterLabel();
        }

        private void Clear()
        {
            _model.Clear();
            HideCounterLabel();
        }

        private void ShowCounterLabel()
        {
            _view.CounterLabel.style.display = DisplayStyle.Flex;
        }

        private void HideCounterLabel()
        {
            _view.CounterLabel.style.display = DisplayStyle.None;
        }

        private void SubscribeToEvents()
        {
            EventsManager.PointsApplied.AddListener(Apply);
            EventsManager.PointsCleared.AddListener(Clear);
        }

        private void Subscribe()
        {
            _view.X1PlusButton.clicked += OnX1PlusButtonClicked;
            _view.X5PlusButton.clicked += OnX5PlusButtonClicked;
            _view.X1MinusButton.clicked += OnX1MinusButtonClicked;
            _view.X5MinusButton.clicked += OnX5MinusButtonClicked;
        }

        public void Dispose()
        {
            _view.X1PlusButton.clicked -= OnX1PlusButtonClicked;
            _view.X5PlusButton.clicked -= OnX5PlusButtonClicked;
            _view.X1MinusButton.clicked -= OnX1MinusButtonClicked;
            _view.X5MinusButton.clicked -= OnX5MinusButtonClicked;

            EventsManager.PointsApplied.RemoveListener(Apply);
            EventsManager.PointsCleared.RemoveListener(Clear);
        }
    }
}
