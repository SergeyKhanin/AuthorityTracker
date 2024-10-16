using System;

namespace Game
{
    public sealed class PausePresenter : IDisposable
    {
        private readonly PauseView _view;

        public PausePresenter(PauseView view)
        {
            _view = view;
        }

        public void Dispose() { }
    }
}
