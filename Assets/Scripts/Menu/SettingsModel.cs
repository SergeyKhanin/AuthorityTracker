using Common;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Menu
{
    public sealed class SettingsModel
    {
        private const int PlayerAmount = (int)Players.Player2;

        public SettingsModel()
        {
            if (PlayerPrefs.HasKey(CommonNames.LanguageName))
            {
                SetLanguage((Languages)PlayerPrefs.GetInt(CommonNames.LanguageName));
            }
            else
            {
                SetLanguage(Languages.English);
                PlayerPrefs.Save();
            }
        }

        public int GetPlayersAmount() => PlayerAmount;

        public void SetLanguage(Languages language)
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

            PlayerPrefs.Save();
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
    }
}
