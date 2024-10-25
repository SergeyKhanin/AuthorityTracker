using Menu;
using Scopes.Initializers;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class MenuLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private UIDocument uiDocument;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(uiDocument);
            builder.Register<SettingsModel>(Lifetime.Singleton);
            builder.Register<MenuInitializer>(Lifetime.Singleton).As<IStartable>();
        }
    }
}
