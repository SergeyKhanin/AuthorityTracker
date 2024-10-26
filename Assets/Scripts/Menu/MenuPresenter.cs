using System;
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
            BindLocalizations();
            UpdateContinueButtonVisibility();
            HideQuitButtonOnWeb();
        }

        private void HideQuitButtonOnWeb()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                _view.QuitButton.Hide();
        }

        private void OnStartButtonClicked()
        {
            _model.DeletePlayersData();
            SceneManager.LoadScene((int)Scenes.GameScene);
        }

        private void OnSettingButtonClicked() => EventsManager.SettingsOpened.Invoke();

        private void OnContinueButtonClicked() => SceneManager.LoadScene((int)Scenes.GameScene);

        private void OnQuitButtonClicked() => Application.Quit();

        private void Show() => _view.Container.Show();

        private void Hide() => _view.Container.Hide();

        private void UpdateContinueButtonVisibility() =>
            _view.ContinueButton.SetVisibility(_model.HasPlayersData());

        private void Subscribe()
        {
            _view.StartButton.clicked += OnStartButtonClicked;
            _view.SettingsButton.clicked += OnSettingButtonClicked;
            _view.ContinueButton.clicked += OnContinueButtonClicked;
            _view.QuitButton.clicked += OnQuitButtonClicked;

            EventsManager.SettingsOpened.AddListener(Hide);
            EventsManager.SettingsClosed.AddListener(Show);
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

        private void BindLocalizations()
        {
            _view.StartButton.BindLocalization(LocalizationKeys.Buttons.Start);
            _view.SettingsButton.BindLocalization(LocalizationKeys.Buttons.Settings);
            _view.ContinueButton.BindLocalization(LocalizationKeys.Buttons.Continue);
            _view.QuitButton.BindLocalization(LocalizationKeys.Buttons.Quit);
        }
    }
}
