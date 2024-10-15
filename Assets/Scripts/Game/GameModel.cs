namespace Game
{
    public sealed class GameModel
    {
        public bool IsGamePaused { get; private set; }
        public bool IsConfirmPopupActive { get; private set; }

        public void PauseGame() => IsGamePaused = true;

        public void UnpauseGame() => IsGamePaused = false;

        public void ShowConfirmPopup() => IsConfirmPopupActive = true;

        public void HideConfirmPopup() => IsConfirmPopupActive = false;
    }
}
