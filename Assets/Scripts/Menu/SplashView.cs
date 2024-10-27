using Common;
using UnityEngine.UIElements;

namespace Menu
{
    public sealed class SplashView
    {
        public VisualElement Container { get; private set; }
        public VisualElement ImageFakeContainer { get; private set; }
        public VisualElement ImageContainer { get; private set; }

        public SplashView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(CommonNames.SplashViewName);
            Container = root;
            ImageContainer = root.Q<VisualElement>("image-container");
            ImageFakeContainer = root.Q<VisualElement>("image-fake-container");
        }
    }
}
