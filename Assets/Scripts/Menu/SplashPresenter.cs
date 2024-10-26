using Extensions;

namespace Menu
{
    public sealed class SplashPresenter
    {
        private readonly SplashView _view;

        public SplashPresenter(SplashView view) => _view = view;

        public void Hide() => _view.Container.Hide();

        public void Show() => _view.Container.Show();
    }
}
