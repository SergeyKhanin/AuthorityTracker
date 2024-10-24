using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
using VContainer.Unity;

namespace Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private UIDocument uiDocument;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(uiDocument);
        }
    }
}
