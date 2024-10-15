using Common;
using Player;
using Popup;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bootstrap
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class GameBootstrap : MonoBehaviour
    {
        private UIDocument _uiDocument;

        private void Start()
        {
            _uiDocument = GetComponent<UIDocument>();

            CreatePlayers(CommonPlayers.Player2);
            CreateConfirmPopup();
        }

        private void CreateConfirmPopup()
        {
            var confirmPopupPresenter = new ConfirmPopupPresenter(
                new ConfirmPopupView(_uiDocument)
            );
        }

        private void CreatePlayers(CommonPlayers amount)
        {
            var uiDocument = _uiDocument;
            var root = uiDocument.rootVisualElement;
            var playerTemplate = Resources.Load<VisualTreeAsset>(
                CommonTemplatePath.PlayerTemplatePath
            );

            for (int i = 1; i <= (int)amount; i++)
            {
                var templateName = CommonNames.PlayerViewName + i;
                var template = playerTemplate.Instantiate();

                template.name = templateName;
                root.Add(template);

                var playerPresenter = new PlayerPresenter(
                    new PlayerView(uiDocument, templateName),
                    new PlayerModel()
                );

                playerPresenter.SetName(templateName);

                if (PlayerPrefs.HasKey(templateName))
                {
                    playerPresenter.SetPoints(PlayerPrefs.GetInt(templateName));
                    playerPresenter.UpdatePointsLabel();
                }
            }
        }
    }
}
