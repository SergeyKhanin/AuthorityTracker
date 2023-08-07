using UnityEngine;
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
    private VisualElement _authorityImage;
    private VisualElement _iconPlus;
    private VisualElement _iconMinus;

    private CustomButton _minusButton;
    private CustomButton _plusButton;
    private CustomButton _applyButton;
    private CustomButton _plus5Button;
    private CustomButton _minus10Button;

    private CustomLabel _authorityLabel;
    private CustomLabel _pointsLabel;
    private CustomLabel _pointsMinusLabel;

    private string _playerName;
    private bool _isPlus5ButtonClicked;
    private bool _isMinus10ButtonClicked;
    private bool _isPointsZero;
    private bool _isPointsLong;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _applyButtonView = GetComponent<ApplyButtonView>();
        _playerName = SetPlayers.GetPlayerFromList(playersList);
        _authority = new Authority();

        _frame = _root.Q<VisualElement>(_playerName);

        _authorityImage = _frame.Q<VisualElement>("authority-image");
        _iconPlus = _frame.Q<VisualElement>("icon-plus");
        _iconMinus = _frame.Q<VisualElement>("icon-minus");

        _minusButton = _frame.Q<CustomButton>("minus-button");
        _plusButton = _frame.Q<CustomButton>("plus-button");
        _applyButton = _root.Q<CustomButton>("apply-button");
        _plus5Button = _frame.Q<CustomButton>("plus-5-button");
        _minus10Button = _frame.Q<CustomButton>("minus-10-button");

        _authorityLabel = _frame.Q<CustomLabel>("authority-label");
        _pointsLabel = _frame.Q<CustomLabel>("points-label");

        _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        _iconPlus.AddToClassList(CommonUssClassNames.Hide);
        _iconMinus.AddToClassList(CommonUssClassNames.Hide);
        _authorityLabel.text = _authority.Points.ToString();
    }

    private void OnEnable()
    {
        _plusButton.clicked += OnPlusButtonClicked;
        _minusButton.clicked += OnMinusButtonClicked;
        _applyButton.clicked += OnApplyButtonClicked;
        _plus5Button.clicked += OnPlus5ButtonClicked;
        _minus10Button.clicked += OnMinus10ButtonClicked;
    }

    private void OnDisable()
    {
        _plusButton.clicked -= OnPlusButtonClicked;
        _minusButton.clicked -= OnMinusButtonClicked;
        _applyButton.clicked -= OnApplyButtonClicked;
        _plus5Button.clicked -= OnPlus5ButtonClicked;
        _minus10Button.clicked -= OnMinus10ButtonClicked;
    }

    private void OnPlusButtonClicked()
    {
        if (_isPlus5ButtonClicked)
            Counter += 5;
        else
            Counter++;

        _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
        _iconPlus.RemoveFromClassList(CommonUssClassNames.Hide);
        _applyButtonView.CheckDoneButton();

        UpdatePointsLabel();

        _isPlus5ButtonClicked = false;
    }

    private void OnMinusButtonClicked()
    {
        if (_isMinus10ButtonClicked)
            Counter -= 10;
        else
            Counter--;

        _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
        _iconMinus.RemoveFromClassList(CommonUssClassNames.Hide);
        _applyButtonView.CheckDoneButton();

        UpdatePointsLabel();

        _isMinus10ButtonClicked = false;
    }

    private void OnApplyButtonClicked()
    {
        _authority.AddCustomPoints(Counter);
        _authority.ValidatePoints();

        ValidateText();
        ValidateClasses();

        Counter = 0;

        _authorityLabel.text = _authority.Points.ToString();
        _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        _iconPlus.AddToClassList(CommonUssClassNames.Hide);
        _iconMinus.AddToClassList(CommonUssClassNames.Hide);

        _applyButtonView.CheckDoneButton();
    }

    private void UpdatePointsLabel()
    {
        if (Counter < 0)
        {
            _pointsLabel.text = Counter.ToString().TrimStart('-');
            _pointsLabel.AddToClassList(CommonUssClassNames.PointsLabelMinus);
            _iconPlus.AddToClassList(CommonUssClassNames.Hide);
            _iconMinus.RemoveFromClassList(CommonUssClassNames.Hide);
        }

        else if (Counter == 0)
        {
            _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
            _iconPlus.AddToClassList(CommonUssClassNames.Hide);
            _iconMinus.AddToClassList(CommonUssClassNames.Hide);
        }

        else
        {
            _pointsLabel.text = Counter.ToString();
            _pointsLabel.RemoveFromClassList(CommonUssClassNames.PointsLabelMinus);
            _pointsLabel.AddToClassList(CommonUssClassNames.PointsLabelPlus);
            _iconPlus.RemoveFromClassList(CommonUssClassNames.Hide);
            _iconMinus.AddToClassList(CommonUssClassNames.Hide);
        }
    }

    private void OnPlus5ButtonClicked()
    {
        _isPlus5ButtonClicked = true;
        OnPlusButtonClicked();
    }

    private void OnMinus10ButtonClicked()
    {
        _isMinus10ButtonClicked = true;
        OnMinusButtonClicked();
    }

    private void ValidateClasses()
    {
        _authorityImage.EnableInClassList(CommonUssClassNames.ImageAuthorityRed, _isPointsZero);
        _authorityLabel.EnableInClassList(CommonUssClassNames.LabelAuthoritySizeSmall, _isPointsLong);
    }

    private void ValidateText()
    {
        if (_authority.Points <= 0)
            _isPointsZero = true;
        else
            _isPointsZero = false;
        if (_authority.Points > 99 || _authority.Points < -9)
            _isPointsLong = true;
        else
            _isPointsLong = false;
    }
}