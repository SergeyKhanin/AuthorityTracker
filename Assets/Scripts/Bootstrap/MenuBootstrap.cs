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

        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();

            var menuPresenter = new MenuPresenter(
                new MenuView(_uiDocument, CommonNames.MenuViewName),
                new MenuModel()
            );
        }
    }
}
