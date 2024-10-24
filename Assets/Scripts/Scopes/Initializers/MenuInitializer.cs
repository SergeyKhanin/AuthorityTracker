using System;
using Menu;
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
        public MenuInitializer(UIDocument uiDocument) => _uiDocument = uiDocument;

        private void CreateElements(UIDocument uiDocument)
        {
            var model = new SettingsModel();
            _menuPresenter = new MenuPresenter(new MenuView(uiDocument), model);
            _settingsPresenter = new SettingsPresenter(new SettingsView(uiDocument), model);
        }

        public void Start() => CreateElements(_uiDocument);

        public void Dispose()
        {
            _menuPresenter?.Dispose();
            _settingsPresenter?.Dispose();
        }
    }
}
