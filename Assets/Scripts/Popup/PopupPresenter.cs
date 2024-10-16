using System;
using Events;
using Game;
using UnityEngine.UIElements;

namespace Popup
{
    public sealed class PopupPresenter : IDisposable
    {
        private readonly PopupView _view;

        public PopupPresenter(PopupView view)
        {
            _view = view;

            Subscribe();
            SubscribeToEvents();
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

        private void Show() => _view.Container.style.visibility = Visibility.Visible;

        private void Hide() => _view.Container.style.visibility = Visibility.Hidden;

        private void SubscribeToEvents()
        {
            EventsManager.CounterChanged.AddListener(Show);
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

            EventsManager.CounterChanged.RemoveListener(Show);
        }
    }
}
