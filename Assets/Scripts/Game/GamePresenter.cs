using System;
using Events;
using Extensions;

namespace Game
{
    public sealed class GamePresenter : IDisposable
    {
        private readonly GameView _view;

        public GamePresenter(GameView view)
        {
            _view = view;

            Subscribe();
            SubscribeToEvents();
            HideHistoryCloseButton();
        }

        private void OnPauseButtonClicked() => EventsManager.PauseOpened.Invoke();

        private void OnHistoryOpenButtonClicked() => ShowHistoryCloseButton();

        private void OnHistoryCloseButtonClicked() => HideHistoryCloseButton();

        private void Show() => _view.Container.Show();

        private void Hide() => _view.Container.Hide();

        private void ShowHistoryCloseButton()
        {
            EventsManager.HistoryOpened.Invoke();
            _view.HistoryOpenButton.Hide();
            _view.HistoryCloseButton.Show();
        }

        private void HideHistoryCloseButton()
        {
            EventsManager.HistoryClosed.Invoke();
            _view.HistoryOpenButton.Show();
            _view.HistoryCloseButton.Hide();
        }

        private void SubscribeToEvents()
        {
            EventsManager.PauseOpened.AddListener(Hide);
            EventsManager.PauseClosed.AddListener(Show);
        }

        private void Subscribe()
        {
            _view.PauseButton.clicked += OnPauseButtonClicked;
            _view.HistoryOpenButton.clicked += OnHistoryOpenButtonClicked;
            _view.HistoryCloseButton.clicked += OnHistoryCloseButtonClicked;
        }

        public void Dispose()
        {
            _view.PauseButton.clicked += OnPauseButtonClicked;
            _view.HistoryOpenButton.clicked -= OnHistoryOpenButtonClicked;
            _view.HistoryCloseButton.clicked -= OnHistoryCloseButtonClicked;

            EventsManager.PauseOpened.RemoveListener(Hide);
            EventsManager.PauseClosed.RemoveListener(Show);
        }
    }
}
