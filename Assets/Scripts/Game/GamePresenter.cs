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
            ShowHistoryOpenButton();
        }

        private void OnPauseButtonClicked() => EventsManager.PauseOpened.Invoke();

        private void Show() => _view.Container.Show();

        private void Hide() => _view.Container.Hide();

        private void ShowHistoryOpenButton()
        {
            _view.HistoryOpenButton.Show();
            _view.HistoryCloseButton.Hide();
        }

        private void HideHistoryOpenButton()
        {
            _view.HistoryOpenButton.Hide();
            _view.HistoryCloseButton.Show();
        }

        private void SubscribeToEvents()
        {
            EventsManager.PauseOpened.AddListener(Hide);
            EventsManager.PauseClosed.AddListener(Show);
            EventsManager.HistoryClosed.AddListener(ShowHistoryOpenButton);
            EventsManager.HistoryOpened.AddListener(HideHistoryOpenButton);
        }

        private void Subscribe()
        {
            _view.PauseButton.clicked += OnPauseButtonClicked;
            _view.HistoryOpenButton.clicked += OnPauseButtonClicked;
            _view.HistoryCloseButton.clicked += OnPauseButtonClicked;
        }

        public void Dispose()
        {
            _view.PauseButton.clicked += OnPauseButtonClicked;
            _view.HistoryOpenButton.clicked -= OnPauseButtonClicked;
            _view.HistoryCloseButton.clicked -= OnPauseButtonClicked;

            EventsManager.PauseOpened.RemoveListener(Hide);
            EventsManager.PauseClosed.RemoveListener(Show);
            EventsManager.HistoryClosed.RemoveListener(ShowHistoryOpenButton);
            EventsManager.HistoryOpened.RemoveListener(HideHistoryOpenButton);
        }
    }
}
