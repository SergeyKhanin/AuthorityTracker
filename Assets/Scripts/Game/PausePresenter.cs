using System;
using Common;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void OnQuitButtonClicked() => Application.Quit();

        private void OnMenuButtonClicked() => SceneManager.LoadScene((int)CommonScenes.MenuScene);

        private void OnBackButtonClicked() => EventsManager.PauseClosed.Invoke();

        private void OnResetButtonClicked() => SceneManager.LoadScene((int)CommonScenes.MenuScene);

        private void Hide() => _view.Container.style.visibility = Visibility.Hidden;

        private void Show() => _view.Container.style.visibility = Visibility.Visible;

        private void SubscribeToEvents()
        {
            EventsManager.PauseOpened.AddListener(Hide);
            EventsManager.PauseClosed.AddListener(Show);
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

            EventsManager.PauseOpened.RemoveListener(Hide);
            EventsManager.PauseClosed.RemoveListener(Show);
        }
    }
}
