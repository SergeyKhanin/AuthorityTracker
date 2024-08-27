using UnityEngine;

namespace Player
{
    public class PlayerPresenter : MonoBehaviour
    {
        private PlayerView _view;
        private PlayerModel _model;


        private void Awake()
        {
            _view = GetComponent<PlayerView>();
            _view.Init();

            _view.X1MinusButton.text = "-1";
            _view.X5MinusButton.text = "-5";
            _view.X1PlusButton.text = "+1";
            _view.X5PlusButton.text = "+5";
        }
    }
}