using System;
using Common;
using Events;
using Extensions;
using UnityEngine;

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
            HideCounterContainer();
            UpdatePointsVisualState();
        }

        private void OnX1PlusButtonClicked() => PlusX1();

        private void OnX5PlusButtonClicked() => PlusX5();

        private void OnX1MinusButtonClicked() => MinusX1();

        private void OnX5MinusButtonClicked() => MinusX5();

        private void PlusX1()
        {
            _model.X1Plus();
            UpdateCounterVisualState();
        }

        private void PlusX5()
        {
            _model.X5Plus();
            UpdateCounterVisualState();
        }

        private void MinusX5()
        {
            _model.X5Minus();
            UpdateCounterVisualState();
        }

        private void MinusX1()
        {
            _model.X1Minus();
            UpdateCounterVisualState();
        }

        private void UpdatePointsVisualState()
        {
            _view.PointsImage.ClearClassList();
            _view.PointsImage.AddToClassList(
                CommonUssClassNames.UssPointsImageName
                    + _model.PointsImageVisualState.ToString().ToLower()
            );

            _view.PointsLabel.ClearClassList();
            _view.PointsLabel.AddToClassList(
                CommonUssClassNames.UssPointsLabelName
                    + _model.PointsLabelVisualState.ToString().ToLower()
            );

            Debug.Log(_model.PointsLabelVisualState);

            _view.PointsLabel.text = _model.Points.ToString();
        }

        private void UpdateCounterVisualState()
        {
            EventsManager.CounterChanged.Invoke();
            ShowCounterContainer();

            var value = Math.Abs(_model.Counter);

            _view.CounterLabel.text = value.ToString();
            _view.CounterContainer.ClearClassList();

            var counterStateClassName = _model.CounterVisualState.ToString().ToLower();
            _view.CounterContainer.AddToClassList(counterStateClassName);
        }

        private void Apply()
        {
            _model.Apply();
            UpdatePointsVisualState();
            HideCounterContainer();
        }

        private void Clear()
        {
            _model.Clear();
            HideCounterContainer();
        }

        private void RestartPoints()
        {
            _model.RestartPoints();
            Clear();
            UpdatePointsVisualState();
        }

        private void ShowCounterContainer() => _view.CounterContainer.Show();

        private void HideCounterContainer() => _view.CounterContainer.Hide();

        private void SubscribeToEvents()
        {
            EventsManager.PointsApplied.AddListener(Apply);
            EventsManager.PointsCleared.AddListener(Clear);
            EventsManager.PointsRestarted.AddListener(RestartPoints);
            EventsManager.PauseOpened.AddListener(Clear);
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
            EventsManager.PointsRestarted.RemoveListener(RestartPoints);
            EventsManager.PauseOpened.RemoveListener(Clear);
        }
    }
}
