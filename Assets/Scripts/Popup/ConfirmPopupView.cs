using UnityEngine.UIElements;

namespace Popup
{
    public sealed class ConfirmPopupView
    {
        public VisualElement ButtonsContainer { get; private set; }
        public Button ApplyButton { get; private set; }
        public Button ClearButton { get; private set; }

        public ConfirmPopupView(UIDocument uiDocument, string pathToParent)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(pathToParent);
            ButtonsContainer = root.Q<VisualElement>("buttons-container");
            ApplyButton = root.Q<Button>("apply-button");
            ClearButton = root.Q<Button>("clear-button");
        }
    }
}
