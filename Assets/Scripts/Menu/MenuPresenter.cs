﻿using System;
using Common;
using Events;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public sealed class MenuPresenter : IDisposable
    {
        private readonly MenuView _view;
        private readonly SettingsModel _model;

        public MenuPresenter(MenuView view, SettingsModel model)
        {
            _view = view;
            _model = model;

            Subscribe();
            SubscribeToEvents();
            UpdateContinueButtonVisibility();
        }

        private void OnStartButtonClicked()
        {
            _model.ResetPlayersData();
            SceneManager.LoadScene((int)Scenes.GameScene);
        }

        private void OnSettingButtonClicked() => EventsManager.SettingsOpened.Invoke();

        private void OnContinueButtonClicked() => SceneManager.LoadScene((int)Scenes.GameScene);

        private void OnQuitButtonClicked() => Application.Quit();

        private void Show() => _view.Container.Show();

        private void Hide() => _view.Container.Hide();

        private void UpdateContinueButtonVisibility() =>
            _view.ContinueButton.SetVisibility(_model.HasPlayersData());

        private void SubscribeToEvents()
        {
            EventsManager.SettingsOpened.AddListener(Hide);
            EventsManager.SettingsClosed.AddListener(Show);
        }

        private void Subscribe()
        {
            _view.StartButton.clicked += OnStartButtonClicked;
            _view.SettingsButton.clicked += OnSettingButtonClicked;
            _view.ContinueButton.clicked += OnContinueButtonClicked;
            _view.QuitButton.clicked += OnQuitButtonClicked;
        }

        public void Dispose()
        {
            _view.StartButton.clicked -= OnStartButtonClicked;
            _view.SettingsButton.clicked -= OnSettingButtonClicked;
            _view.ContinueButton.clicked -= OnContinueButtonClicked;
            _view.QuitButton.clicked -= OnQuitButtonClicked;

            EventsManager.SettingsOpened.RemoveListener(Hide);
            EventsManager.SettingsClosed.RemoveListener(Show);
        }
    }
}
