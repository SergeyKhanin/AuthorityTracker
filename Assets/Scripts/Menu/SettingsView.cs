using Common;
using UnityEngine.UIElements;

namespace Menu
{
    public sealed class SettingsView
    {
        public VisualElement Container { get; private set; }
        public VisualElement LangContainer { get; private set; }
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
        public Button IndonesianLanguageButton { get; private set; }
        public Button JapaneseLanguageButton { get; private set; }
        public Button GermanLanguageButton { get; private set; }
        public Button TeluguLanguageButton { get; private set; }
        public Button MarathiLanguageButton { get; private set; }
        public Button TurkishLanguageButton { get; private set; }
        public Button DeleteDataButton { get; private set; }
        public Button BackButton { get; private set; }

        public SettingsView(UIDocument uiDocument)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(CommonNames.SettingsViewName);
            Container = root;
            LangContainer = root.Q<VisualElement>("lang-container");
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
            IndonesianLanguageButton = root.Q<Button>("indonesian-language-button");
            JapaneseLanguageButton = root.Q<Button>("japanese-language-button");
            GermanLanguageButton = root.Q<Button>("german-language-button");
            TeluguLanguageButton = root.Q<Button>("telugu-language-button");
            MarathiLanguageButton = root.Q<Button>("marathi-language-button");
            TurkishLanguageButton = root.Q<Button>("turkish-language-button");
            DeleteDataButton = root.Q<Button>("delete-data-button");
            BackButton = root.Q<Button>("back-button");
        }
    }
}
