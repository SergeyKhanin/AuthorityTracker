using Player;
using UnityEngine;
using Zenject;

public class StartupScript : MonoBehaviour
{
    [Inject]
    private PlayerPresenter _playerPresenter;

    void Start()
    {
        // PlayerPresenter будет автоматически инжектирован Zenject'ом
        // Здесь вы можете начать использовать _playerPresenter
    }
}
