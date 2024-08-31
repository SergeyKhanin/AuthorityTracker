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
        }

        public void Init() => Subscribe();

        private void OnX1PlusButtonClicked() => UpdatePointsLabel();

        private void OnX5PlusButtonClicked() => UpdatePointsLabel();

        private void OnX1MinusButtonClicked() => UpdatePointsLabel();

        private void OnX5MinusButtonClicked() => UpdatePointsLabel();

        private void Subscribe()
        {
            _view.Init();
            _view.X1PlusButton.clicked += OnX1PlusButtonClicked;
            _view.X5PlusButton.clicked += OnX5PlusButtonClicked;
            _view.X1MinusButton.clicked += OnX1MinusButtonClicked;
            _view.X5MinusButton.clicked += OnX5MinusButtonClicked;
        }

        public void Dispose()
        {
            // _view.X1PlusButton.clicked -= OnX1PlusButtonClicked;
            // _view.X5PlusButton.clicked -= OnX5PlusButtonClicked;
            // _view.X1MinusButton.clicked -= OnX1MinusButtonClicked;
            // _view.X5MinusButton.clicked -= OnX5MinusButtonClicked;
        }

        private void UpdatePointsLabel() => Debug.Log("Сlicked");
    }
}
