using Menu;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class MenuBootstrap : MonoBehaviour
    {
        private MenuPresenter _menuPresenter;
        private SettingsPresenter _settingsPresenter;

        private void Start() => CreateElements(GetUiDocument());

        private UIDocument GetUiDocument() => GetComponent<UIDocument>();

        private void CreateElements(UIDocument document)
        {
            var model = new SettingsModel();
            _menuPresenter = new MenuPresenter(new MenuView(document), model);
            _settingsPresenter = new SettingsPresenter(new SettingsView(document), model);
        }

        private void OnDestroy()
        {
            _menuPresenter?.Dispose();
            _settingsPresenter?.Dispose();
        }
    }
}
