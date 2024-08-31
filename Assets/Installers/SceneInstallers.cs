using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class SceneInstallers : MonoInstaller
    {
        [SerializeField]
        PlayerView _playerView;

        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle();
            Container.Bind<PlayerPresenter>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle();
        }
    }
}
