using Common;
using UnityEngine.UIElements;

namespace Menu
{
    public sealed class SplashView
    {
        public VisualElement Container { get; private set; }

        public SplashView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(CommonNames.SplashViewName);
            Container = root;
        }
    }
}
