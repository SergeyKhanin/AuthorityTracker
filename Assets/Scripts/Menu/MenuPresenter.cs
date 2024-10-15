using System;
using Common;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
        }

        private void OnStartButtonClicked() => SceneManager.LoadScene((int)CommonScenes.GameScene);

        private void OnSettingButtonClicked() => GameEventsManager.SettingsOpened.Invoke();

        private void OnContinueButtonClicked() => Application.Quit();

        private void OnResetButtonClicked()
        {
            _model.ResetData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnQuitButtonClicked() => Application.Quit();

        private void ShowMenu() => _view.Container.style.visibility = Visibility.Visible;

        private void HideMenu() => _view.Container.style.visibility = Visibility.Hidden;

        private void SubscribeToEvents()
        {
            GameEventsManager.SettingsOpened.AddListener(HideMenu);
            GameEventsManager.SettingsClosed.AddListener(ShowMenu);
        }

        private void Subscribe()
        {
            _view.StartButton.clicked += OnStartButtonClicked;
            _view.SettingsButton.clicked += OnSettingButtonClicked;
            _view.ContinueButton.clicked += OnContinueButtonClicked;
            _view.ResetButton.clicked += OnResetButtonClicked;
            _view.QuitButton.clicked += OnQuitButtonClicked;
        }

        public void Dispose()
        {
            _view.StartButton.clicked -= OnStartButtonClicked;
            _view.SettingsButton.clicked -= OnSettingButtonClicked;
            _view.ContinueButton.clicked -= OnContinueButtonClicked;
            _view.ResetButton.clicked -= OnResetButtonClicked;
            _view.QuitButton.clicked -= OnQuitButtonClicked;

            GameEventsManager.SettingsOpened.RemoveListener(HideMenu);
            GameEventsManager.SettingsClosed.RemoveListener(ShowMenu);
        }
    }
}
