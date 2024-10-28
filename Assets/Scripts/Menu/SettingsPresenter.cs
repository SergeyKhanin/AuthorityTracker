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
        private readonly Dictionary<Button, Languages> _languageButtons = new();
        private readonly Dictionary<Button, Action> _languageButtonsClickHandlers = new();

        public SettingsPresenter(SettingsView view, SettingsModel model)
        {
            _view = view;
            _model = model;

            InitializeLanguageButtons();
            Subscribe();
            BindLocalizations();
            UpdateLanguageButtonStates();
        }

        private void OnBackButtonClicked() => EventsManager.SettingsClosed.Invoke();

        private void OnDeleteDataButtonClicked()
        {
            _model.DeleteData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Hide() => _view.Container.Hide();

        private void Show() => _view.Container.Show();

        private void UpdateLanguageButtonStates()
        {
            foreach (var button in _languageButtons)
            {
                button.Key.EnableInClassList(
                    CommonUssClassNames.UssSelectedName,
                    button.Value == _model.Languages
                );
            }
        }

        private void InitializeLanguageButtons()
        {
            _languageButtons.Add(_view.EnglishLanguageButton, Languages.English);
            _languageButtons.Add(_view.ChineseLanguageButton, Languages.Chinese);
            _languageButtons.Add(_view.HindiLanguageButton, Languages.Hindi);
            _languageButtons.Add(_view.SpanishLanguageButton, Languages.Spanish);
            _languageButtons.Add(_view.FrenchLanguageButton, Languages.French);
            _languageButtons.Add(_view.ArabicLanguageButton, Languages.Arabic);
            _languageButtons.Add(_view.BanglaLanguageButton, Languages.Bangla);
            _languageButtons.Add(_view.PortugueseLanguageButton, Languages.Portuguese);
            _languageButtons.Add(_view.RussianLanguageButton, Languages.Russian);
            _languageButtons.Add(_view.UrduLanguageButton, Languages.Urdu);
            _languageButtons.Add(_view.IndonesianLanguageButton, Languages.Indonesian);
            _languageButtons.Add(_view.JapaneseLanguageButton, Languages.Japanese);
            _languageButtons.Add(_view.GermanLanguageButton, Languages.German);
            _languageButtons.Add(_view.TeluguLanguageButton, Languages.Telugu);
            _languageButtons.Add(_view.MarathiLanguageButton, Languages.Marathi);
            _languageButtons.Add(_view.TurkishLanguageButton, Languages.Turkish);
        }

        private void OnLanguageButtonClicked(Languages language)
        {
            _model.SetLanguage(language);
            UpdateLanguageButtonStates();
        }

        private void BindLocalizations()
        {
            _view.BackButton.BindLocalization(LocalizationKeys.Buttons.Back);
            _view.DeleteDataButton.BindLocalization(LocalizationKeys.Buttons.DeleteData);
        }

        private void Subscribe()
        {
            foreach (var button in _languageButtons)
            {
                var language = button.Value;
                Action clickHandler = () => OnLanguageButtonClicked(language);
                _languageButtonsClickHandlers[button.Key] = clickHandler;
                button.Key.clicked += clickHandler;
            }

            _view.DeleteDataButton.clicked += OnDeleteDataButtonClicked;
            _view.BackButton.clicked += OnBackButtonClicked;

            EventsManager.SettingsOpened.AddListener(Show);
            EventsManager.SettingsClosed.AddListener(Hide);
        }

        public void Dispose()
        {
            foreach (var button in _languageButtons)
            {
                if (_languageButtonsClickHandlers.TryGetValue(button.Key, out var clickHandler))
                {
                    button.Key.clicked -= clickHandler;
                }
            }

            _view.DeleteDataButton.clicked -= OnDeleteDataButtonClicked;
            _view.BackButton.clicked -= OnBackButtonClicked;

            EventsManager.SettingsOpened.RemoveListener(Show);
            EventsManager.SettingsClosed.RemoveListener(Hide);
        }
    }
}
