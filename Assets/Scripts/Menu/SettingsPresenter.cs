using System;
using Common;
using Events;
using UnityEngine.UIElements;

namespace Menu
{
    public sealed class SettingsPresenter : IDisposable
    {
        private readonly SettingsView _view;
        private readonly SettingsModel _model;

        public SettingsPresenter(SettingsView view, SettingsModel model)
        {
            _view = view;
            _model = model;

            Subscribe();
            SubscribeToEvents();
            Hide();
        }

        private void OnEnglishLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.English);

        private void OnChineseLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Chinese);

        private void OnHindiLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Hindi);

        private void OnSpanishLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Spanish);

        private void OnFrenchLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.French);

        private void OnArabicLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Arabic);

        private void OnBanglaLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Bangla);

        private void OnPortugueseLanguageButtonClicked() =>
            _model.SetLanguage(CommonLanguage.Portuguese);

        private void OnRussianLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Russian);

        private void OnUrduLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Urdu);

        private void OnIndonesianLanguageButtonClicked() =>
            _model.SetLanguage(CommonLanguage.Indonesian);

        private void OnJapaneseLanguageButtonClicked() =>
            _model.SetLanguage(CommonLanguage.Japanese);

        private void OnGermanLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.German);

        private void OnTeluguLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Telugu);

        private void OnMarathiLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Marathi);

        private void OnTurkishLanguageButtonClicked() => _model.SetLanguage(CommonLanguage.Turkish);

        private void OnBackButtonClicked() => EventsManager.SettingsClosed.Invoke();

        private void Hide() => _view.Container.style.visibility = Visibility.Hidden;

        private void Show() => _view.Container.style.visibility = Visibility.Visible;

        private void SubscribeToEvents()
        {
            EventsManager.SettingsOpened.AddListener(Show);
            EventsManager.SettingsClosed.AddListener(Hide);
        }

        private void Subscribe()
        {
            _view.EnglishLanguageButton.clicked += OnEnglishLanguageButtonClicked;
            _view.ChineseLanguageButton.clicked += OnChineseLanguageButtonClicked;
            _view.HindiLanguageButton.clicked += OnHindiLanguageButtonClicked;
            _view.SpanishLanguageButton.clicked += OnSpanishLanguageButtonClicked;
            _view.FrenchLanguageButton.clicked += OnFrenchLanguageButtonClicked;
            _view.ArabicLanguageButton.clicked += OnArabicLanguageButtonClicked;
            _view.BanglaLanguageButton.clicked += OnBanglaLanguageButtonClicked;
            _view.PortugueseLanguageButton.clicked += OnPortugueseLanguageButtonClicked;
            _view.RussianLanguageButton.clicked += OnRussianLanguageButtonClicked;
            _view.UrduLanguageButton.clicked += OnUrduLanguageButtonClicked;
            _view.IndonesianLanguageButton.clicked += OnIndonesianLanguageButtonClicked;
            _view.JapaneseLanguageButton.clicked += OnJapaneseLanguageButtonClicked;
            _view.GermanLanguageButton.clicked += OnGermanLanguageButtonClicked;
            _view.TeluguLanguageButton.clicked += OnTeluguLanguageButtonClicked;
            _view.MarathiLanguageButton.clicked += OnMarathiLanguageButtonClicked;
            _view.TurkishLanguageButton.clicked += OnTurkishLanguageButtonClicked;
            _view.BackButton.clicked += OnBackButtonClicked;
        }

        public void Dispose()
        {
            _view.EnglishLanguageButton.clicked -= OnEnglishLanguageButtonClicked;
            _view.ChineseLanguageButton.clicked -= OnChineseLanguageButtonClicked;
            _view.HindiLanguageButton.clicked -= OnHindiLanguageButtonClicked;
            _view.SpanishLanguageButton.clicked -= OnSpanishLanguageButtonClicked;
            _view.FrenchLanguageButton.clicked -= OnFrenchLanguageButtonClicked;
            _view.ArabicLanguageButton.clicked -= OnArabicLanguageButtonClicked;
            _view.BanglaLanguageButton.clicked -= OnBanglaLanguageButtonClicked;
            _view.PortugueseLanguageButton.clicked -= OnPortugueseLanguageButtonClicked;
            _view.RussianLanguageButton.clicked -= OnRussianLanguageButtonClicked;
            _view.UrduLanguageButton.clicked -= OnUrduLanguageButtonClicked;
            _view.IndonesianLanguageButton.clicked -= OnIndonesianLanguageButtonClicked;
            _view.JapaneseLanguageButton.clicked -= OnJapaneseLanguageButtonClicked;
            _view.GermanLanguageButton.clicked -= OnGermanLanguageButtonClicked;
            _view.TeluguLanguageButton.clicked -= OnTeluguLanguageButtonClicked;
            _view.MarathiLanguageButton.clicked -= OnMarathiLanguageButtonClicked;
            _view.TurkishLanguageButton.clicked -= OnTurkishLanguageButtonClicked;
            _view.BackButton.clicked -= OnBackButtonClicked;

            EventsManager.SettingsOpened.RemoveListener(Show);
            EventsManager.SettingsClosed.RemoveListener(Hide);
        }
    }
}
