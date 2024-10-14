using UnityEngine.UIElements;

namespace Menu
{
    public sealed class LanguageView
    {
        public Button EnglishLanguageButton { get; private set; }
        public Button ChineseLanguageButton { get; private set; }
        public Button HindiLanguageButton { get; private set; }
        public Button SpanishLanguageButton { get; private set; }
        public Button FrenchLanguageButton { get; private set; }
        public Button ArabicLanguageButton { get; private set; }
        public Button BanglaLanguageButton { get; private set; }
        public Button PortugueseLanguageButton { get; private set; }
        public Button RussianLanguageButton { get; private set; }
        public Button UrduLanguageButton { get; private set; }

        public LanguageView(UIDocument uiDocument, string pathToParent)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(pathToParent);
            EnglishLanguageButton = root.Q<Button>("english-language-button");
            ChineseLanguageButton = root.Q<Button>("chinese-language-button");
            HindiLanguageButton = root.Q<Button>("hindi-language-button");
            SpanishLanguageButton = root.Q<Button>("spanish-language-button");
            FrenchLanguageButton = root.Q<Button>("french-language-button");
            ArabicLanguageButton = root.Q<Button>("arabic-language-button");
            BanglaLanguageButton = root.Q<Button>("bangla-language-button");
            PortugueseLanguageButton = root.Q<Button>("portuguese-language-button");
            RussianLanguageButton = root.Q<Button>("russian-language-button");
            UrduLanguageButton = root.Q<Button>("urdu-language-button");
        }
    }
}
