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
            if (_playerView == null)
                Debug.LogError($"{nameof(SceneInstallers)}.{nameof(_playerView)} is null.");
            else
                Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle();

            Container.Bind<PlayerPresenter>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle();

            Debug.Log($"{nameof(SceneInstallers)} installed.");
        }
    }
}
