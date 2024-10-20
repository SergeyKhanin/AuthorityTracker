using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Menu
{
    public sealed class SettingsModel
    {
        private const int PlayerAmount = (int)Players.Player2;
        public Languages Languages { get; private set; }

        private Dictionary<Languages, Locale> _locales;

        public SettingsModel()
        {
            InitializeLanguageLocales();

            if (PlayerPrefs.HasKey(CommonNames.LanguageName))
            {
                Languages = (Languages)PlayerPrefs.GetInt(CommonNames.LanguageName);
                SetLanguage(Languages);
            }
            else
            {
                SetLanguage(Languages.English);
            }
        }

        public int GetPlayersAmount() => PlayerAmount;

        public void SetLanguage(Languages language)
        {
            LocalizationSettings.SelectedLocale = GetLocal(language);

            Languages = language;

            SaveLanguage();
        }

        public void ResetPlayersData()
        {
            for (int i = 1; i <= PlayerAmount; i++)
            {
                if (PlayerPrefs.HasKey(CommonNames.PlayerName + i))
                {
                    PlayerPrefs.DeleteKey(CommonNames.PlayerName + i);
                    PlayerPrefs.DeleteKey(CommonNames.MaxPointsName + CommonNames.PlayerName + i);
                }
            }
        }

        public bool HasPlayersData()
        {
            for (int i = 1; i <= PlayerAmount; i++)
            {
                if (PlayerPrefs.HasKey(CommonNames.PlayerName + i))
                    return true;
            }

            return false;
        }

        public void ResetData() => PlayerPrefs.DeleteAll();

        private void InitializeLanguageLocales()
        {
            _locales = new Dictionary<Languages, Locale>();
            var settings = LocalizationSettings.AvailableLocales;

            _locales.Add(Languages.English, settings.GetLocale(LanguagesIdentifier.En));
            _locales.Add(Languages.Chinese, settings.GetLocale(LanguagesIdentifier.Zh));
            _locales.Add(Languages.Hindi, settings.GetLocale(LanguagesIdentifier.Hi));
            _locales.Add(Languages.Spanish, settings.GetLocale(LanguagesIdentifier.Es));
            _locales.Add(Languages.French, settings.GetLocale(LanguagesIdentifier.Fr));
            _locales.Add(Languages.Arabic, settings.GetLocale(LanguagesIdentifier.Ar));
            _locales.Add(Languages.Bangla, settings.GetLocale(LanguagesIdentifier.Bn));
            _locales.Add(Languages.Portuguese, settings.GetLocale(LanguagesIdentifier.Pt));
            _locales.Add(Languages.Russian, settings.GetLocale(LanguagesIdentifier.Ru));
            _locales.Add(Languages.Urdu, settings.GetLocale(LanguagesIdentifier.Ur));
            _locales.Add(Languages.Indonesian, settings.GetLocale(LanguagesIdentifier.Id));
            _locales.Add(Languages.Japanese, settings.GetLocale(LanguagesIdentifier.Ja));
            _locales.Add(Languages.German, settings.GetLocale(LanguagesIdentifier.De));
            _locales.Add(Languages.Telugu, settings.GetLocale(LanguagesIdentifier.Te));
            _locales.Add(Languages.Marathi, settings.GetLocale(LanguagesIdentifier.Mr));
            _locales.Add(Languages.Turkish, settings.GetLocale(LanguagesIdentifier.Tr));
        }

        private Locale GetLocal(Languages language)
        {
            return language switch
            {
                Languages.English => _locales[Languages.English],
                Languages.Chinese => _locales[Languages.Chinese],
                Languages.Hindi => _locales[Languages.Hindi],
                Languages.Spanish => _locales[Languages.Spanish],
                Languages.French => _locales[Languages.French],
                Languages.Arabic => _locales[Languages.Arabic],
                Languages.Bangla => _locales[Languages.Bangla],
                Languages.Portuguese => _locales[Languages.Portuguese],
                Languages.Russian => _locales[Languages.Russian],
                Languages.Urdu => _locales[Languages.Urdu],
                Languages.Indonesian => _locales[Languages.Indonesian],
                Languages.Japanese => _locales[Languages.Japanese],
                Languages.German => _locales[Languages.German],
                Languages.Telugu => _locales[Languages.Telugu],
                Languages.Marathi => _locales[Languages.Marathi],
                Languages.Turkish => _locales[Languages.Turkish],
                _ => _locales[Languages.English]
            };
        }

        private void SaveLanguage()
        {
            PlayerPrefs.SetInt(CommonNames.LanguageName, (int)Languages);
            PlayerPrefs.Save();
        }
    }
}
