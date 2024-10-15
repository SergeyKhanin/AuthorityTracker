using System;
using Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public sealed class MenuPresenter : IDisposable
    {
        private readonly MenuView _view;
        private readonly MenuModel _model;

        public MenuPresenter(MenuView view, MenuModel model)
        {
            _view = view;
            _model = model;

            Subscribe();
        }

        private void OnStartButtonClicked() => SceneManager.LoadScene((int)CommonScenes.GameScene);

        private void OnContinueButtonClicked() => _model.OpenSettings();

        private void OnSettingButtonClicked() => _model.OpenSettings();

        private void OnCommunityButtonClicked() => _model.OpenSettings();

        private void OnQuitButtonClicked() => Application.Quit();

        private void Subscribe()
        {
            _view.StartButton.clicked += OnStartButtonClicked;
            _view.ContinueButton.clicked += OnContinueButtonClicked;
            _view.SettingsButton.clicked += OnSettingButtonClicked;
            _view.CommunityButton.clicked += OnCommunityButtonClicked;
            _view.QuitButton.clicked += OnQuitButtonClicked;
        }

        public void Dispose()
        {
            _view.StartButton.clicked -= OnStartButtonClicked;
            _view.ContinueButton.clicked -= OnContinueButtonClicked;
            _view.SettingsButton.clicked -= OnSettingButtonClicked;
            _view.CommunityButton.clicked -= OnCommunityButtonClicked;
            _view.QuitButton.clicked -= OnQuitButtonClicked;
        }
    }
}
