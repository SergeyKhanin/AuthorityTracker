using Common;
using UnityEngine.UIElements;

namespace Popup
{
    public sealed class ConfirmPopupView
    {
        public VisualElement Container { get; private set; }
        public Button ApplyButton { get; private set; }
        public Button ClearButton { get; private set; }

        public ConfirmPopupView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(
                CommonNames.ConfirmPopupViewName
            );
            Container = root;
            ApplyButton = root.Q<Button>("apply-button");
            ClearButton = root.Q<Button>("clear-button");
        }
    }
}
