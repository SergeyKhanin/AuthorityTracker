using System;
using Events;
using Game;
using UnityEngine.UIElements;

namespace Popup
{
    public sealed class ConfirmPopupPresenter : IDisposable
    {
        private readonly ConfirmPopupView _view;
        private readonly GameModel _model;

        public ConfirmPopupPresenter(ConfirmPopupView view, GameModel model)
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
            _view.ContentContainer.style.visibility = Visibility.Visible;
            _model.ShowConfirmPopup();
        }

        private void HideConfirmPopupView()
        {
            _view.ContentContainer.style.visibility = Visibility.Hidden;
            _model.HideConfirmPopup();
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
