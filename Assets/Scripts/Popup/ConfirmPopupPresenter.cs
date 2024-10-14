using System;
using Events;
using UnityEngine.UIElements;

namespace Popup
{
    public sealed class ConfirmPopupPresenter : IDisposable
    {
        private readonly ConfirmPopupView _view;
        private readonly ConfirmPopupModel _model;

        public ConfirmPopupPresenter(ConfirmPopupView view, ConfirmPopupModel model)
        {
            _view = view;
            _model = model;

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

        private void ShowConfirmPopupView()
        {
            _view.ButtonsContainer.style.visibility = Visibility.Visible;
        }

        private void HideConfirmPopupView()
        {
            _view.ButtonsContainer.style.visibility = Visibility.Hidden;
        }

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
