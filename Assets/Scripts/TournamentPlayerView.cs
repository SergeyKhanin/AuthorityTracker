using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[RequireComponent(typeof(ApplyButtonView))]
public class TournamentPlayerView : MonoBehaviour
{
    public int Counter { get; private set; }

    [SerializeField] private PlayersRoster.PlayersList playersList;

    private Authority _authority;
    private ApplyButtonView _applyButtonView;
    private VisualElement _root;
    private VisualElement _frame;
    private CustomButton _minusButton;
    private CustomButton _plusButton;
    private CustomButton _applyButton;
    private VisualElement _pointsMinusContainer;

    private CustomLabel _authorityLabel;
    private CustomLabel _pointsLabel;
    private CustomLabel _pointsMinusLabel;
    
    private string _playerName;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _applyButtonView = GetComponent<ApplyButtonView>();
        _playerName = SetPlayers.GetPlayerFromList(playersList);
        _authority = new Authority();

        _frame = _root.Q<VisualElement>(_playerName);

        _minusButton = _frame.Q<CustomButton>("minus-button");
        _plusButton = _frame.Q<CustomButton>("plus-button");
        _applyButton = _root.Q<CustomButton>("apply-button");

        _authorityLabel = _frame.Q<CustomLabel>("authority-label");
        _pointsLabel = _frame.Q<CustomLabel>("points-label");

        _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        _authorityLabel.text = _authority.Points.ToString();
    }

    private void OnEnable()
    {
        _plusButton.clicked += OnPlusButtonClicked;
        _minusButton.clicked += OnMinusButtonClicked;
        _applyButton.clicked += OnApplyButtonClicked;
    }

    private void OnDisable()
    {
        _plusButton.clicked -= OnPlusButtonClicked;
        _minusButton.clicked -= OnMinusButtonClicked;
        _applyButton.clicked -= OnApplyButtonClicked;
    }

    private void OnPlusButtonClicked()
    {
        Counter++;

        _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
        _applyButtonView.CheckDoneButton();

        CheckLabels();
    }

    private void OnMinusButtonClicked()
    {
        Counter--;

        _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
        _applyButtonView.CheckDoneButton();

        CheckLabels();
    }

    private void OnApplyButtonClicked()
    {
        _authority.Points += Counter;

        Counter = 0;

        _authorityLabel.text = _authority.Points.ToString();
        _pointsLabel.AddToClassList(CommonUssClassNames.Hide);

        _applyButtonView.CheckDoneButton();
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