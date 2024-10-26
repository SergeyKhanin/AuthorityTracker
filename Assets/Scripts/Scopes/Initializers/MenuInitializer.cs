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
        private readonly SettingsModel _model;

        [Inject]
        public MenuInitializer(UIDocument uiDocument, SettingsModel model)
        {
            _uiDocument = uiDocument;
            _model = model;
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
            _model.Init();
            _menuPresenter = new MenuPresenter(new MenuView(_uiDocument), _model);
            _settingsPresenter = new SettingsPresenter(new SettingsView(_uiDocument), _model);
        }

        public void Dispose()
        {
            _menuPresenter?.Dispose();
            _settingsPresenter?.Dispose();
        }
    }
}
