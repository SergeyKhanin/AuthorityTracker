using System;
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
            _view.PointsLabel.text = _model.Points.ToString();
        }

        private void OnX1PlusButtonClicked() => PlusX1();

        private void OnX5PlusButtonClicked() => PlusX5();

        private void OnX1MinusButtonClicked() => MinusX1();

        private void OnX5MinusButtonClicked() => MinusX5();

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
            Debug.Log($"{_view.Container.name}: Clicked - Score is: {_model.Points}");
            _view.PointsLabel.text = _model.Points.ToString();
        }
    }
}
