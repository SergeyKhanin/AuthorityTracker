using System;
using Common;
using Events;
using Extensions;
using UnityEngine.SceneManagement;

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

        private void OnEnglishLanguageButtonClicked() => _model.SetLanguage(Languages.English);

        private void OnChineseLanguageButtonClicked() => _model.SetLanguage(Languages.Chinese);

        private void OnHindiLanguageButtonClicked() => _model.SetLanguage(Languages.Hindi);

        private void OnSpanishLanguageButtonClicked() => _model.SetLanguage(Languages.Spanish);

        private void OnFrenchLanguageButtonClicked() => _model.SetLanguage(Languages.French);

        private void OnArabicLanguageButtonClicked() => _model.SetLanguage(Languages.Arabic);

        private void OnBanglaLanguageButtonClicked() => _model.SetLanguage(Languages.Bangla);

        private void OnPortugueseLanguageButtonClicked() =>
            _model.SetLanguage(Languages.Portuguese);

        private void OnRussianLanguageButtonClicked() => _model.SetLanguage(Languages.Russian);

        private void OnUrduLanguageButtonClicked() => _model.SetLanguage(Languages.Urdu);

        private void OnIndonesianLanguageButtonClicked() =>
            _model.SetLanguage(Languages.Indonesian);

        private void OnJapaneseLanguageButtonClicked() => _model.SetLanguage(Languages.Japanese);

        private void OnGermanLanguageButtonClicked() => _model.SetLanguage(Languages.German);

        private void OnTeluguLanguageButtonClicked() => _model.SetLanguage(Languages.Telugu);

        private void OnMarathiLanguageButtonClicked() => _model.SetLanguage(Languages.Marathi);

        private void OnTurkishLanguageButtonClicked() => _model.SetLanguage(Languages.Turkish);

        private void OnBackButtonClicked() => EventsManager.SettingsClosed.Invoke();

        private void OnCleatDataButtonClicked()
        {
            _model.ResetData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Hide() => _view.Container.Hide();

        private void Show() => _view.Container.Show();

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
            _view.CleatDataButton.clicked += OnCleatDataButtonClicked;
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
            _view.CleatDataButton.clicked -= OnCleatDataButtonClicked;
            _view.BackButton.clicked -= OnBackButtonClicked;

            EventsManager.SettingsOpened.RemoveListener(Show);
            EventsManager.SettingsClosed.RemoveListener(Hide);
        }
    }
}
