using Common;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Menu
{
    public sealed class MenuModel
    {
        public void SetLanguage(CommonLanguage language)
        {
            var index = (int)language;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                index
            ];
            SaveLanguage(index);
        }

        private void SaveLanguage(int index)
        {
            PlayerPrefs.SetInt(CommonNames.LanguageName, index);
            PlayerPrefs.Save();
        }

        public void ResetData() => PlayerPrefs.DeleteAll();
    }
}
