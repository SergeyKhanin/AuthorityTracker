using UnityEngine;
using UnityEngine.UIElements;

public class TournamentPlayerView : MonoBehaviour
{
    public int Counter { get; private set; }

    [SerializeField] private PlayersRoster.PlayersList playersList;

    private Authority _authority;
    private DoneButtonView _doneButtonView;
    private VisualElement _root;
    private VisualElement _frame;
    private string _playerName;
    private CustomButton _minusButton;
    private CustomButton _plusButton;
    private CustomButton _doneButton;
    private VisualElement _pointsMinusContainer;

    private CustomLabel _authorityLabel;
    private CustomLabel _pointsLabel;
    private CustomLabel _pointsMinusLabel;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _doneButtonView = GetComponent<DoneButtonView>();

        _authority = new Authority();

        _playerName = SetPlayers.GetPlayerFromList(playersList);

        _frame = _root.Q<VisualElement>(_playerName);

        _minusButton = _frame.Q<CustomButton>("minus-button");
        _plusButton = _frame.Q<CustomButton>("plus-button");
        _doneButton = _root.Q<CustomButton>("done-button");

        _authorityLabel = _frame.Q<CustomLabel>("authority-label");
        _pointsLabel = _frame.Q<CustomLabel>("points-label");

        _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        _authorityLabel.text = _authority.Points.ToString();
    }

    private void OnEnable()
    {
        _plusButton.clicked += OnPlusButtonClicked;
        _minusButton.clicked += OnMinusButtonClicked;
        _doneButton.clicked += OnDoneButtonClicked;
    }

    private void OnDisable()
    {
        _plusButton.clicked -= OnPlusButtonClicked;
        _minusButton.clicked -= OnMinusButtonClicked;
        _doneButton.clicked -= OnDoneButtonClicked;
    }

    private void OnPlusButtonClicked()
    {
        Counter++;

        _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
        _doneButtonView.CheckDoneButton();

        CheckLabels();
    }

    private void OnMinusButtonClicked()
    {
        Counter--;

        _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
        _doneButtonView.CheckDoneButton();

        CheckLabels();
    }

    private void OnDoneButtonClicked()
    {
        _authority.Points += Counter;

        Counter = 0;

        _authorityLabel.text = _authority.Points.ToString();
        _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        _doneButton.AddToClassList(CommonUssClassNames.Hide);
    }

    private void CheckLabels()
    {
        if (Counter < 0)
        {
            _pointsLabel.text = Counter.ToString().TrimStart('-');
            _pointsLabel.AddToClassList(CommonUssClassNames.PointsLabelMinus);
        }

        else if (Counter == 0)
        {
            _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        }

        else
        {
            _pointsLabel.text = Counter.ToString();
            _pointsLabel.RemoveFromClassList(CommonUssClassNames.PointsLabelMinus);
            _pointsLabel.AddToClassList(CommonUssClassNames.PointsLabelPlus);
        }
    }
}