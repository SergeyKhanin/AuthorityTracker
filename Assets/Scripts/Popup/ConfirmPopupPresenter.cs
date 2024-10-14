using System;
using Events;
using UnityEngine;

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
        }

        private void ApplyButtonOnClicked() => GameEventsManager.ApplyPoints.Invoke();

        private void ClearButtonOnClicked() => GameEventsManager.ClearPoints.Invoke();

        private void Subscribe()
        {
            _view.ApplyButton.clicked += ApplyButtonOnClicked;
            _view.ClearButton.clicked += ClearButtonOnClicked;
        }

        public void Dispose()
        {
            _view.ApplyButton.clicked -= ApplyButtonOnClicked;
            _view.ClearButton.clicked -= ClearButtonOnClicked;
        }
    }
}
