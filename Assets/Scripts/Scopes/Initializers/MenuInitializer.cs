using System;
using Menu;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Scopes.Initializers
{
    public sealed class MenuInitializer : IStartable, IDisposable
    {
        private SplashPresenter _splashPresenter;
        private MenuPresenter _menuPresenter;
        private SettingsPresenter _settingsPresenter;
        private readonly UIDocument _uiDocument;

        [Inject]
        public MenuInitializer(UIDocument uiDocument)
        {
            _uiDocument = uiDocument;
        }

        public void Start()
        {
            _splashPresenter = new SplashPresenter(new SplashView(_uiDocument));

            if (Application.platform == RuntimePlatform.WebGLPlayer)
                _splashPresenter.Show();

            var init = LocalizationSettings.InitializationOperation;
            init.Completed += a =>
            {
                _splashPresenter.Hide();
                CreateElements();
                AllowScreenSleep();
            };
        }

        private void AllowScreenSleep() => Screen.sleepTimeout = SleepTimeout.SystemSetting;

        private void CreateElements()
        {
            var model = new SettingsModel();

            _menuPresenter = new MenuPresenter(new MenuView(_uiDocument), model);
            _settingsPresenter = new SettingsPresenter(new SettingsView(_uiDocument), model);
        }

        public void Dispose()
        {
            _menuPresenter?.Dispose();
            _settingsPresenter?.Dispose();
        }
    }
}
