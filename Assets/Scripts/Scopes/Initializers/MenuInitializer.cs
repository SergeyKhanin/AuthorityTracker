using System;
using System.Threading.Tasks;
using Menu;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Scopes.Initializers
{
    public sealed class MenuInitializer : IStartable, IAsyncDisposable
    {
        private SplashPresenter _splashPresenter;
        private MenuPresenter _menuPresenter;
        private SettingsPresenter _settingsPresenter;
        private AsyncOperationHandle<LocalizationSettings> _initializationOperation;
        private readonly UIDocument _uiDocument;

        [Inject]
        public MenuInitializer(UIDocument uiDocument)
        {
            _uiDocument = uiDocument;
        }

        public void Start()
        {
            _splashPresenter = new SplashPresenter(new SplashView(_uiDocument));
            _splashPresenter.Show();

            _initializationOperation = LocalizationSettings.InitializationOperation;
            _initializationOperation.Completed += OnLocalizationInitialized;
        }

        private void OnLocalizationInitialized(AsyncOperationHandle<LocalizationSettings> handle)
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
                return;
            _splashPresenter.Hide();
            CreateElements();
            AllowScreenSleep();
        }

        private void AllowScreenSleep() => Screen.sleepTimeout = SleepTimeout.SystemSetting;

        private void CreateElements()
        {
            var model = new SettingsModel();

            _menuPresenter = new MenuPresenter(new MenuView(_uiDocument), model);
            _settingsPresenter = new SettingsPresenter(new SettingsView(_uiDocument), model);
        }

        public async ValueTask DisposeAsync()
        {
            if (_initializationOperation.IsValid())
                _initializationOperation.Completed -= OnLocalizationInitialized;

            if (_initializationOperation.Status == AsyncOperationStatus.None)
                await _initializationOperation.Task;

            _menuPresenter?.Dispose();
            _settingsPresenter?.Dispose();
        }
    }
}
