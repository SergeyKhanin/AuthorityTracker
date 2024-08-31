using Player;
using UnityEngine;
using Zenject;

public class StartupScript : MonoBehaviour
{
    [Inject]
    private PlayerPresenter _playerPresenter;

    void Start() => _playerPresenter.Init();
}
