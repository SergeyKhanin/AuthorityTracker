using System;
using System.Collections.Generic;
using Common;
using Events;
using Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Menu
{
    public sealed class SettingsPresenter : IDisposable
    {
        private readonly SettingsView _view;
        private readonly SettingsModel _model;
        private List<Button> _languagesButtons = new();

        public SettingsPresenter(SettingsView view, SettingsModel model)
        {
            _view = view;
            _model = model;

            Subscribe();
            SubscribeToEvents();
            GetLanguagesButtonsList();
            UpdateLanguageButtonStates();
            Hide();
        }

        private void OnEnglishLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.English);
            UpdateLanguageButtonStates();
        }

        private void OnChineseLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Chinese);
            UpdateLanguageButtonStates();
        }

        private void OnHindiLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Hindi);
            UpdateLanguageButtonStates();
        }

        private void OnSpanishLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Spanish);
            UpdateLanguageButtonStates();
        }

        private void OnFrenchLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.French);
            UpdateLanguageButtonStates();
        }

        private void OnArabicLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Arabic);
            UpdateLanguageButtonStates();
        }

        private void OnBanglaLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Bangla);
            UpdateLanguageButtonStates();
        }

        private void OnPortugueseLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Portuguese);
            UpdateLanguageButtonStates();
        }

        private void OnRussianLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Russian);
            UpdateLanguageButtonStates();
        }

        private void OnUrduLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Urdu);
            UpdateLanguageButtonStates();
        }

        private void OnIndonesianLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Indonesian);
            UpdateLanguageButtonStates();
        }

        private void OnJapaneseLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Japanese);
            UpdateLanguageButtonStates();
        }

        private void OnGermanLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.German);
            UpdateLanguageButtonStates();
        }

        private void OnTeluguLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Telugu);
            UpdateLanguageButtonStates();
        }

        private void OnMarathiLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Marathi);
            UpdateLanguageButtonStates();
        }

        private void OnTurkishLanguageButtonClicked()
        {
            _model.SetLanguage(Languages.Turkish);
            UpdateLanguageButtonStates();
        }

        private void OnBackButtonClicked() => EventsManager.SettingsClosed.Invoke();

        private void OnCleatDataButtonClicked()
        {
            _model.ResetData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Hide() => _view.Container.Hide();

        private void Show() => _view.Container.Show();

        private void GetLanguagesButtonsList() =>
            _languagesButtons = _view.LangContainer.Query<Button>().ToList();

        private void UpdateLanguageButtonStates()
        {
            foreach (var button in _languagesButtons)
            {
                button.EnableInClassList(
                    CommonUssClassNames.UssSelectedName,
                    button.name.Contains(_model.Languages.ToString().ToLower())
                );
            }
        }

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
