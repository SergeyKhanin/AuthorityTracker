using System;
using Menu;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
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
        private AsyncOperationHandle<LocalizationSettings> _locHandle;
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
            Init();
            AllowScreenSleep();
        }

        private void Init()
        {
            _locHandle = LocalizationSettings.InitializationOperation;
            _locHandle.Completed += InitializeLocales;
        }

        private void InitializeLocales(AsyncOperationHandle<LocalizationSettings> handle)
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
                return;

            CreateElements();
        }

        private void AllowScreenSleep() => Screen.sleepTimeout = SleepTimeout.SystemSetting;

        private void CreateElements()
        {
            _model.InitializeLanguageLocales();
            _splashPresenter = new SplashPresenter(new SplashView(_uiDocument));
            _menuPresenter = new MenuPresenter(new MenuView(_uiDocument), _model);
            _settingsPresenter = new SettingsPresenter(new SettingsView(_uiDocument), _model);
            _splashPresenter.Hide();
        }

        public void Dispose()
        {
            _menuPresenter?.Dispose();
            _settingsPresenter?.Dispose();

            if (_locHandle.IsValid())
                _locHandle.Release();
        }
    }
}
