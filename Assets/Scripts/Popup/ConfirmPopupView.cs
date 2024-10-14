using UnityEngine.UIElements;

namespace Popup
{
    public sealed class ConfirmPopupView
    {
        public Button ApplyButton { get; private set; }
        public Button ClearButton { get; private set; }

        public ConfirmPopupView(UIDocument uiDocument, string pathToParent)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(pathToParent);
            ApplyButton = root.Q<Button>("apply-button");
            ClearButton = root.Q<Button>("clear-button");
        }
    }
}
