using System;

namespace PopUp
{
    public sealed class ConfirmPopupPresenter : IDisposable
    {
        private readonly ConfirmPopupView _view;
        private readonly ConfirmPopupModel _model;

        public ConfirmPopupPresenter(ConfirmPopupView view, ConfirmPopupModel model)
        {
            _view = view;
            _model = model;
        }

        private void DoSome() { }

        private void Subscribe()
        {
            _view.ApplyButton.clicked += DoSome;
            _view.ClearButton.clicked += DoSome;
        }

        public void Dispose()
        {
            _view.ApplyButton.clicked -= DoSome;
            _view.ClearButton.clicked -= DoSome;
        }
    }
}
