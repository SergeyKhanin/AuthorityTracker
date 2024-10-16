﻿using System;
using Events;
using UnityEngine.UIElements;

namespace Game
{
    public sealed class GamePresenter : IDisposable
    {
        private readonly GameView _view;

        public GamePresenter(GameView view)
        {
            _view = view;

            Subscribe();
            SubscribeToEvents();
        }

        private void OnPauseButtonClicked() => EventsManager.PauseOpened.Invoke();

        private void Show() => _view.Container.style.visibility = Visibility.Visible;

        private void Hide() => _view.Container.style.visibility = Visibility.Hidden;

        private void SubscribeToEvents()
        {
            EventsManager.PauseOpened.AddListener(Hide);
            EventsManager.PauseClosed.AddListener(Show);
        }

        private void Subscribe()
        {
            _view.PauseButton.clicked += OnPauseButtonClicked;
        }

        public void Dispose()
        {
            _view.PauseButton.clicked += OnPauseButtonClicked;

            EventsManager.PauseOpened.RemoveListener(Hide);
            EventsManager.PauseClosed.RemoveListener(Show);
        }
    }
}
