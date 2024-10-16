using System;

namespace Game
{
    public sealed class GamePresenter : IDisposable
    {
        private readonly GameView _view;

        public GamePresenter(GameView view)
        {
            _view = view;
        }

        public void Dispose() { }
    }
}
