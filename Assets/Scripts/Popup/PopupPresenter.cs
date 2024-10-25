using System;
using Common;
using Events;
using Extensions;

namespace Popup
{
    public sealed class PopupPresenter : IDisposable
    {
        private readonly PopupView _view;

        public PopupPresenter(PopupView view)
        {
            _view = view;

            Subscribe();
            BindLocalizations();
            Hide();
        }

        private void ApplyButtonOnClicked()
        {
            EventsManager.PointsApplied.Invoke();
            Hide();
        }

        private void ClearButtonOnClicked()
        {
            EventsManager.PointsCleared.Invoke();
            Hide();
        }

        private void Show() => _view.Container.Show();

        private void Hide() => _view.Container.Hide();

        private void Subscribe()
        {
            _view.ApplyButton.clicked += ApplyButtonOnClicked;
            _view.ClearButton.clicked += ClearButtonOnClicked;

            EventsManager.CounterChanged.AddListener(Show);
            EventsManager.PauseOpened.AddListener(Hide);
            EventsManager.HistoryOpened.AddListener(Hide);
        }

        public void Dispose()
        {
            _view.ApplyButton.clicked -= ApplyButtonOnClicked;
            _view.ClearButton.clicked -= ClearButtonOnClicked;

            EventsManager.CounterChanged.RemoveListener(Show);
            EventsManager.PauseOpened.RemoveListener(Hide);
            EventsManager.HistoryOpened.RemoveListener(Hide);
        }

        private void BindLocalizations()
        {
            _view.ApplyButton.BindLocalization(LocalizationKeys.Buttons.Apply);
            _view.ClearButton.BindLocalization(LocalizationKeys.Buttons.Clear);
        }
    }
}
