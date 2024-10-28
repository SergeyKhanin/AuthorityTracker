using System;
using Menu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
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
        private AsyncOperationHandle<StringTable> _enTableHandle;
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
            _enTableHandle = Addressables.LoadAssetAsync<StringTable>("AuthorityTracker_en-US");
            _enTableHandle.Completed += handle =>
                Debug.Log(
                    handle.Status == AsyncOperationStatus.Succeeded
                        ? "AuthorityTracker_en-US is completed successfully"
                        : "AuthorityTracker_en-US is failed"
                );
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
            if (_enTableHandle.IsValid())
                _enTableHandle.Release();
        }
    }
}
