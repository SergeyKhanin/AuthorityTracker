using System;

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

        private void Init()
        {
            _view.Init();
            Subscribe();
        }

        private void OnX1PlusButtonClicked() => _model.X1Plus();

        private void OnX5PlusButtonClicked() => _model.X5Plus();

        private void OnX1MinusButtonClicked() => _model.X1Minus();

        private void OnX5MinusButtonClicked() => _model.X5Minus();

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
    }
}
