using System;
using Events;
using Game;
using UnityEngine.UIElements;

namespace Popup
{
    public sealed class ConfirmPopupPresenter : IDisposable
    {
        private readonly ConfirmPopupView _view;

        public ConfirmPopupPresenter(ConfirmPopupView view)
        {
            _view = view;

            Subscribe();
            SubscribeToEvents();
            HideConfirmPopupView();
        }

        private void ApplyButtonOnClicked()
        {
            GameEventsManager.PointsApplied.Invoke();
            HideConfirmPopupView();
        }

        private void ClearButtonOnClicked()
        {
            GameEventsManager.PointsCleared.Invoke();
            HideConfirmPopupView();
        }

        private void ShowConfirmPopupView() =>
            _view.Container.style.visibility = Visibility.Visible;

        private void HideConfirmPopupView() => _view.Container.style.visibility = Visibility.Hidden;

        private void SubscribeToEvents()
        {
            GameEventsManager.CounterChanged.AddListener(ShowConfirmPopupView);
        }

        private void Subscribe()
        {
            _view.ApplyButton.clicked += ApplyButtonOnClicked;
            _view.ClearButton.clicked += ClearButtonOnClicked;
        }

        public void Dispose()
        {
            _view.ApplyButton.clicked -= ApplyButtonOnClicked;
            _view.ClearButton.clicked -= ClearButtonOnClicked;

            GameEventsManager.CounterChanged.RemoveListener(ShowConfirmPopupView);
        }
    }
}
