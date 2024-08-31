using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public sealed class PlayerPresenter : IDisposable
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _model;

        [Inject]
        public PlayerPresenter(PlayerView view, PlayerModel model)
        {
            _view = view;
            _model = model;
            Debug.Log("PlayerPresenter initialized");
        }

        public void Init()
        {
            _view.Init();
            Subscribe();
        }

        private void OnX1PlusButtonClicked()
        {
            _model.X1Plus();
            UpdatePointsLabel();
        }

        private void OnX5PlusButtonClicked()
        {
            _model.X5Plus();
            UpdatePointsLabel();
        }

        private void OnX1MinusButtonClicked()
        {
            _model.X1Minus();
            UpdatePointsLabel();
        }

        private void OnX5MinusButtonClicked()
        {
            _model.X5Minus();
            UpdatePointsLabel();
        }

        private void Subscribe()
        {
            _view.X1PlusButton.clickable.clicked += OnX1PlusButtonClicked;
            _view.X5PlusButton.clickable.clicked += OnX5PlusButtonClicked;
            _view.X1MinusButton.clickable.clicked += OnX1MinusButtonClicked;
            _view.X5MinusButton.clickable.clicked += OnX5MinusButtonClicked;
        }

        public void Dispose()
        {
            _view.X1PlusButton.clickable.clicked -= OnX1PlusButtonClicked;
            _view.X5PlusButton.clickable.clicked -= OnX5PlusButtonClicked;
            _view.X1MinusButton.clickable.clicked -= OnX1MinusButtonClicked;
            _view.X5MinusButton.clickable.clicked -= OnX5MinusButtonClicked;
        }

        private void UpdatePointsLabel()
        {
            _view.PointsLabel.text = _model.Points.ToString();
            Debug.Log("OnClick");
        }
    }
}
