using UnityEngine.UIElements;

namespace PopUp
{
    public sealed class ConfirmPopupView
    {
        public VisualElement Container { get; }
        public Button ApplyButton { get; private set; }
        public Button ClearButton { get; private set; }

        public ConfirmPopupView(UIDocument uiDocument, string pathToParent)
        {
            Container = uiDocument.rootVisualElement.Q<VisualElement>(pathToParent);
            ApplyButton = Container.Q<Button>("apply-button");
            ClearButton = Container.Q<Button>("clear-button");
        }
    }
}
