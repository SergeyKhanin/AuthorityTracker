using Common;
using Menu;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class MenuBootstrap : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private MenuModel _model;

        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();
            _model = new MenuModel();

            if (PlayerPrefs.HasKey(CommonNames.LanguageName))
            {
                _model.SetLanguage((CommonLanguage)PlayerPrefs.GetInt(CommonNames.LanguageName));
            }

            var menuPresenter = new MenuPresenter(
                new MenuView(_uiDocument, CommonNames.MenuViewName),
                _model
            );

            var settingsPresenter = new SettingsPresenter(
                new SettingsView(_uiDocument, CommonNames.SettingsViewName),
                _model
            );
        }
    }
}
