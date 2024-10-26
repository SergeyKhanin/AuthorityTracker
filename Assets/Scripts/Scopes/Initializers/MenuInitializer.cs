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
            var init = LocalizationSettings.InitializationOperation;
            init.Completed += a =>
            {
                CreateElements();
                AllowScreenSleep();
            };
        }

        private void AllowScreenSleep() => Screen.sleepTimeout = SleepTimeout.SystemSetting;

        private void CreateElements()
        {
            var view = _uiDocument;
            var model = new SettingsModel();
            _menuPresenter = new MenuPresenter(new MenuView(view), model);
            _settingsPresenter = new SettingsPresenter(new SettingsView(view), model);
        }

        public void Dispose()
        {
            _menuPresenter?.Dispose();
            _settingsPresenter?.Dispose();
        }
    }
}
