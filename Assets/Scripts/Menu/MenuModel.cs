namespace Menu
{
    public sealed class MenuModel
    {
        public bool IsSettingsOpen { get; private set; }

        public void OpenSettings() => IsSettingsOpen = true;

        public void CloseSettings() => IsSettingsOpen = false;
    }
}
