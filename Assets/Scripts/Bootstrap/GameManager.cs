using Common;
using Player;
using Popup;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class GameManager : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();

            CreateConfirmPopup();
            CreatePlayers(PlayersAmount.Player2);
        }

        private void CreateConfirmPopup()
        {
            var root = _uiDocument.rootVisualElement;
            var confirmPopupTemplate = Resources
                .Load<VisualTreeAsset>(CommonTemplatePath.ConfirmPopupPlatePath)
                .Instantiate();

            confirmPopupTemplate.name = CommonNames.ConfirmPopupName;
            root.Add(confirmPopupTemplate);

            var confirmPopup = new ConfirmPopupPresenter(
                new ConfirmPopupView(_uiDocument, CommonNames.ConfirmPopupName),
                new ConfirmPopupModel()
            );
        }

        private void CreatePlayers(PlayersAmount amount)
        {
            var uiDocument = _uiDocument;
            var root = uiDocument.rootVisualElement;
            var playerTemplate = Resources.Load<VisualTreeAsset>(
                CommonTemplatePath.PlayerTemplatePath
            );

            for (int i = 1; i <= (int)amount; i++)
            {
                var templateName = CommonNames.PlayerName + i;
                var template = playerTemplate.Instantiate();

                template.name = templateName;
                root.Add(template);

                var player = new PlayerPresenter(
                    new PlayerView(uiDocument, templateName),
                    new PlayerModel()
                );
            }
        }
    }
}
