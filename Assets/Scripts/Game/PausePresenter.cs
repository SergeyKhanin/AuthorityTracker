using System;
using Events;
using UnityEngine.UIElements;

namespace Game
{
    public sealed class PausePresenter : IDisposable
    {
        private readonly PauseView _view;

        public PausePresenter(PauseView view)
        {
            _view = view;

            Hide();
            Subscribe();
            SubscribeToEvents();
        }

        private void OnQuitButtonClicked() => throw new NotImplementedException();

        private void OnMenuButtonClicked() => throw new NotImplementedException();

        private void OnResetButtonClicked() => throw new NotImplementedException();

        private void OnBackButtonClicked() => GameEventsManager.PauseClosed.Invoke();

        private void Hide() => _view.Container.style.visibility = Visibility.Hidden;

        private void Show() => _view.Container.style.visibility = Visibility.Visible;

        private void SubscribeToEvents()
        {
            GameEventsManager.PauseOpened.AddListener(Hide);
            GameEventsManager.PauseClosed.AddListener(Show);
        }

        private void Subscribe()
        {
            _view.BackButton.clicked += OnBackButtonClicked;
            _view.ResetButton.clicked += OnResetButtonClicked;
            _view.MenuButton.clicked += OnMenuButtonClicked;
            _view.QuitButton.clicked += OnQuitButtonClicked;
        }

        public void Dispose()
        {
            _view.BackButton.clicked += OnBackButtonClicked;
            _view.ResetButton.clicked += OnResetButtonClicked;
            _view.MenuButton.clicked += OnMenuButtonClicked;
            _view.QuitButton.clicked += OnQuitButtonClicked;

            GameEventsManager.PauseOpened.RemoveListener(Hide);
            GameEventsManager.PauseClosed.RemoveListener(Show);
        }
    }
}
