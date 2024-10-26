﻿using System;
using Common;
using Events;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            BindLocalizations();
            HideQuitButtonOnWeb();
        }

        private void HideQuitButtonOnWeb()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                _view.QuitButton.Hide();
        }

        private void OnQuitButtonClicked() => Application.Quit();

        private void OnMenuButtonClicked() => SceneManager.LoadScene((int)Scenes.MenuScene);

        private void OnBackButtonClicked() => EventsManager.PauseClosed.Invoke();

        private void OnRestartButtonClicked()
        {
            EventsManager.PointsRestarted.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Hide() => _view.Container.Hide();

        private void Show() => _view.Container.Show();

        private void Subscribe()
        {
            _view.BackButton.clicked += OnBackButtonClicked;
            _view.RestartButton.clicked += OnRestartButtonClicked;
            _view.MenuButton.clicked += OnMenuButtonClicked;
            _view.QuitButton.clicked += OnQuitButtonClicked;

            EventsManager.PauseOpened.AddListener(Show);
            EventsManager.PauseClosed.AddListener(Hide);
        }

        public void Dispose()
        {
            _view.BackButton.clicked += OnBackButtonClicked;
            _view.RestartButton.clicked += OnRestartButtonClicked;
            _view.MenuButton.clicked += OnMenuButtonClicked;
            _view.QuitButton.clicked += OnQuitButtonClicked;

            EventsManager.PauseOpened.RemoveListener(Show);
            EventsManager.PauseClosed.RemoveListener(Hide);
        }

        private void BindLocalizations()
        {
            _view.BackButton.BindLocalization(LocalizationKeys.Buttons.Back);
            _view.RestartButton.BindLocalization(LocalizationKeys.Buttons.Restart);
            _view.MenuButton.BindLocalization(LocalizationKeys.Buttons.Menu);
            _view.QuitButton.BindLocalization(LocalizationKeys.Buttons.Quit);
        }
    }
}
