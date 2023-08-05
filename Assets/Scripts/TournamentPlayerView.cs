using UnityEngine;
using UnityEngine.UIElements;

public class TournamentPlayerView : MonoBehaviour
{
    [SerializeField] private PlayersRoster.PlayersList playersRoster;

    private Authority _authority;
    private VisualElement _root;
    private VisualElement _frame;
    private string _playerName;
    private CustomButton _minusButton;
    private CustomButton _plusButton;
    private VisualElement _pointsMinusContainer;

    private int _counter;
    private CustomLabel _authorityLabel;
    private CustomLabel _pointsLabel;
    private CustomLabel _pointsMinusLabel;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _authority = new Authority();

        SetPlayer();

        _frame = _root.Q<VisualElement>(_playerName);

        _minusButton = _frame.Q<CustomButton>("minus-button");
        _plusButton = _frame.Q<CustomButton>("plus-button");

        _authorityLabel = _frame.Q<CustomLabel>("authority-label");
        _pointsLabel = _frame.Q<CustomLabel>("points-label");
    }

    private void OnEnable()
    {
        _plusButton.clicked += OnPlusButtonClicked;
        _minusButton.clicked += OnMinusButtonClicked;
    }

    private void OnDisable()
    {
        _plusButton.clicked -= OnPlusButtonClicked;
        _minusButton.clicked -= OnMinusButtonClicked;
    }

    private void OnPlusButtonClicked()
    {
        _counter++;
        _pointsLabel.text = _counter.ToString();
    }

    private void OnMinusButtonClicked()
    {
        _counter--;
        _pointsLabel.text = _counter.ToString();
    }

    private void SetPlayer()
    {
        switch (playersRoster)
        {
            case PlayersRoster.PlayersList.Player1:
                _playerName = "player-1";
                break;
            case PlayersRoster.PlayersList.Player2:
                _playerName = "player-2";
                break;
            default:
                _playerName = "player-1";
                break;
        }
    }
}