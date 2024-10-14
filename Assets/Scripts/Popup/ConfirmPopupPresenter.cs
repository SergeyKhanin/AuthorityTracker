using System;
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

        private void ApplyButtonOnClicked()
        {
            Debug.LogWarning("ApplyButtonOnClicked");
        }

        private void ClearButtonOnClicked()
        {
            Debug.LogWarning("ClearButtonOnClicked");
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
        }
    }
}
