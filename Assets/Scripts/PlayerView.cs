using UnityEngine;
using UnityEngine.UIElements;

public class PlayerView : MonoBehaviour
{
    private const float TimeDelay = 1.0f;

    [SerializeField] private PlayersRoster.PlayersList playersRoster;

    private Authority _authority;
    private string _playerName;
    private int _plusCounter;
    private int _minusCounter;
    private float _plusPointsTime;
    private float _minusPointsTime;
    private bool _isPointsZero;
    private bool _isPointsLong;
    private bool _isPlus5ButtonClicked;
    private bool _isMinus5ButtonClicked;

    private VisualElement _root;
    private CustomLabel _authorityLabel;
    private CustomLabel _pointsPlusLabel;
    private CustomLabel _pointsMinusLabel;
    private CustomButton _minusButton;
    private CustomButton _plusButton;
    private CustomButton _plus5Button;
    private CustomButton _minus5Button;
    private VisualElement _pointsPlusContainer;
    private VisualElement _pointsMinusContainer;
    private VisualElement _authorityImage;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _authority = new Authority();

        SetPlayer();

        _authorityLabel = _root.Q<VisualElement>(_playerName).Q<CustomLabel>("authority-label");
        _pointsPlusLabel = _root.Q<VisualElement>(_playerName).Q<CustomLabel>("points-plus-label");
        _pointsMinusLabel = _root.Q<VisualElement>(_playerName).Q<CustomLabel>("points-minus-label");
        _authorityImage = _root.Q<VisualElement>(_playerName).Q<VisualElement>("authority-image");

        _minusButton = _root.Q<VisualElement>(_playerName).Q<CustomButton>("minus-button");
        _plusButton = _root.Q<VisualElement>(_playerName).Q<CustomButton>("plus-button");
        _plus5Button = _root.Q<VisualElement>(_playerName).Q<CustomButton>("plus-5-button");
        _minus5Button = _root.Q<VisualElement>(_playerName).Q<CustomButton>("minus-5-button");

        _pointsPlusContainer = _root.Q<VisualElement>(_playerName).Q<VisualElement>("points-plus-container");
        _pointsMinusContainer = _root.Q<VisualElement>(_playerName).Q<VisualElement>("points-minus-container");

        _pointsPlusContainer.AddToClassList(CommonUssClassNames.Invisible);
        _pointsMinusContainer.AddToClassList(CommonUssClassNames.Invisible);
        _authorityLabel.text = _authority.Points.ToString();
    }

    private void Start()
    {
        ValidateText();
        ValidateClasses();
    }

    private void Update()
    {
        _plusPointsTime -= Time.deltaTime;
        _minusPointsTime -= Time.deltaTime;

        UpdatePlusPointsContainer();
        UpdateMinusPointsContainer();
    }

    private void OnEnable()
    {
        _plusButton.clicked += OnPlusButtonClicked;
        _minusButton.clicked += OnMinusButtonClicked;
        _plus5Button.clicked += OnPlus5ButtonClicked;
        _minus5Button.clicked += OnMinus5ButtonClicked;
    }

    private void OnDisable()
    {
        _plusButton.clicked -= OnPlusButtonClicked;
        _minusButton.clicked -= OnMinusButtonClicked;
        _plus5Button.clicked -= OnPlus5ButtonClicked;
        _minus5Button.clicked -= OnMinus5ButtonClicked;
    }

    private void OnPlusButtonClicked()
    {
        _plusPointsTime = 0;

        if (_isPlus5ButtonClicked)
        {
            _authority.PlusFivePoints();
            _plusCounter += 5;
        }
        else
        {
            _authority.PlusPoint();
            _plusCounter++;
        }

        _authority.ValidatePoints();

        _pointsPlusContainer.RemoveFromClassList(CommonUssClassNames.Invisible);
        _authorityLabel.text = _authority.Points.ToString();
        _pointsPlusLabel.text = _plusCounter.ToString();

        AddTimeDelay(ref _plusPointsTime);
        ValidateText();
        ValidateClasses();

        _isPlus5ButtonClicked = false;
    }

    private void OnPlus5ButtonClicked()
    {
        _isPlus5ButtonClicked = true;
        OnPlusButtonClicked();
    }

    private void OnMinusButtonClicked()
    {
        _minusPointsTime = 0;

        if (_isMinus5ButtonClicked)
        {
            _authority.MinusFivePoints();
            _minusCounter += 5;
        }
        else
        {
            _authority.MinusPoint();
            _minusCounter++;
        }

        _authority.ValidatePoints();

        _pointsMinusContainer.RemoveFromClassList(CommonUssClassNames.Invisible);
        _authorityLabel.text = _authority.Points.ToString();
        _pointsMinusLabel.text = _minusCounter.ToString();
        _authority.ValidatePoints();

        AddTimeDelay(ref _minusPointsTime);
        ValidateText();
        ValidateClasses();

        _isMinus5ButtonClicked = false;
    }

    private void OnMinus5ButtonClicked()
    {
        _isMinus5ButtonClicked = true;
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

    private void AddTimeDelay(ref float time)
    {
        time += TimeDelay;
    }

    private void UpdateMinusPointsContainer()
    {
        if (_minusPointsTime < 0)
        {
            _minusCounter = 0;
            _pointsMinusContainer.AddToClassList(CommonUssClassNames.Invisible);
        }
    }

    private void UpdatePlusPointsContainer()
    {
        if (_plusPointsTime < 0)
        {
            _plusCounter = 0;
            _pointsPlusContainer.AddToClassList(CommonUssClassNames.Invisible);
        }
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