﻿using Extensions;

namespace Menu
{
    public sealed class SplashPresenter
    {
        private readonly SplashView _view;

        public SplashPresenter(SplashView view)
        {
            _view = view;
            _view.Container.Hide();
        }
    }
}
