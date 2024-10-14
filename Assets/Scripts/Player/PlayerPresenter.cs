using System;
using Events;
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
            Init();
        }

        private void Init()
        {
            GameEventsManager.ApplyPoints.AddListener(Apply);
            GameEventsManager.ClearPoints.AddListener(Clear);
        }

        private void OnX1PlusButtonClicked() => PlusX1();

        private void OnX5PlusButtonClicked() => PlusX5();

        private void OnX1MinusButtonClicked() => MinusX1();

        private void OnX5MinusButtonClicked() => MinusX5();

        private void PlusX1()
        {
            _model.X1Plus();
            UpdatePointsLabel();
        }

        private void PlusX5()
        {
            _model.X5Plus();
            UpdatePointsLabel();
        }

        private void MinusX5()
        {
            _model.X5Minus();
            UpdatePointsLabel();
        }

        private void MinusX1()
        {
            _model.X1Minus();
            UpdatePointsLabel();
        }

        private void UpdatePointsLabel()
        {
            _view.CounterLabel.text = _model.Points.ToString();
        }

        private void Apply()
        {
            Debug.LogError("Apply");
        }

        private void Clear()
        {
            Debug.LogError("Clear");
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
        }
    }
}
