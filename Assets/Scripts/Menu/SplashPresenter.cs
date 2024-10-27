using Extensions;

namespace Menu
{
    public sealed class SplashPresenter
    {
        private readonly SplashView _view;

        public SplashPresenter(SplashView view) => _view = view;

        public void Hide()
        {
            _view.ImageContainer.style.opacity = 0;
            _view.ImageFakeContainer.Hide();
        }

        public void Show()
        {
            _view.ImageContainer.style.opacity = 1;
            _view.ImageFakeContainer.Show();
        }
    }
}
