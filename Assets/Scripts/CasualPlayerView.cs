using UnityEngine;
using UnityEngine.UIElements;

public class CasualPlayerView : MonoBehaviour
{
    private const float TimeDelay = 1.0f;
    private string _playerName;

    [SerializeField] private PlayersRoster.PlayersList playersList;

    private Authority _authority;

    private int _plusCounter;
    private int _minusCounter;
    private int _startPoints;
    private int _maxPoints;
    private float _plusPointsTime;
    private float _minusPointsTime;
    private bool _isPlus5ButtonClicked;
    private bool _isMinus5ButtonClicked;
    private bool _isPointsLong;
    private bool _isPointsLessZero;
    private bool _isPointsMoreZero;
    private bool _isPointsLessHalf;
    private bool _isPointsLessQuarter;

    private VisualElement _root;
    private VisualElement _frame;
    private CustomLabel _authorityLabel;
    private CustomLabel _pointsPlusLabel;
    private CustomLabel _pointsMinusLabel;
    private CustomButton _minusButton;
    private CustomButton _plusButton;
    private CustomButton _plus5Button;
    private CustomButton _minus5Button;
    private VisualElement _pointsPlusContainer;
    private VisualElement _pointsIconsContainer;
    private VisualElement _pointsMinusContainer;
    private VisualElement _authorityImage;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _playerName = SetPlayers.GetPlayerFromList(playersList);
        _authority = new Authority();

        _frame = _root.Q<VisualElement>(_playerName);

        _authorityImage = _frame.Q<VisualElement>("authority-image");
        _pointsIconsContainer = _frame.Q<VisualElement>("points-icons-container");
        _pointsPlusContainer = _frame.Q<VisualElement>("points-plus-container");
        _pointsMinusContainer = _frame.Q<VisualElement>("points-minus-container");

        _authorityLabel = _frame.Q<CustomLabel>("authority-label");
        _pointsPlusLabel = _frame.Q<CustomLabel>("points-plus-label");
        _pointsMinusLabel = _frame.Q<CustomLabel>("points-minus-label");

        _minusButton = _frame.Q<CustomButton>("minus-button");
        _plusButton = _frame.Q<CustomButton>("plus-button");
        _plus5Button = _frame.Q<CustomButton>("plus-5-button");
        _minus5Button = _frame.Q<CustomButton>("minus-5-button");

        _pointsPlusContainer.AddToClassList(CommonUssClassNames.Invisible);
        _pointsMinusContainer.AddToClassList(CommonUssClassNames.Invisible);

        if (PlayerPrefs.HasKey(_playerName))
            _authority.Points = PlayerPrefs.GetInt(_playerName);

        _authorityLabel.text = _authority.Points.ToString();
        _startPoints = _authority.Points;

        if (PlayerPrefs.HasKey(_playerName + CommonSaveParameters.MaxPoints))
            _maxPoints = PlayerPrefs.GetInt(_playerName + CommonSaveParameters.MaxPoints);
        else
            _maxPoints = _startPoints;
    }

    private void Start()
    {
        SetPointsIconsOpacityValue();
        ValidatePoints();
        ValidateText();
        ValidateClasses();
        SavePlayerAuthority(_playerName, _authority.Points);
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

        ValidatePoints();
        ValidateText();
        ValidateClasses();
        SavePlayerAuthority(_playerName, _authority.Points);

        _isPlus5ButtonClicked = false;
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

        ValidatePoints();
        ValidateText();
        ValidateClasses();
        SavePlayerAuthority(_playerName, _authority.Points);

        _isMinus5ButtonClicked = false;
    }

    private void OnPlus5ButtonClicked()
    {
        _isPlus5ButtonClicked = true;
        OnPlusButtonClicked();
    }

    private void OnMinus5ButtonClicked()
    {
        _isMinus5ButtonClicked = true;
        OnMinusButtonClicked();
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

    private void ValidateClasses()
    {
        _authorityImage.EnableInClassList(CommonUssClassNames.ImageAuthorityOrange, _isPointsLessHalf);
        _authorityImage.EnableInClassList(CommonUssClassNames.ImageAuthorityRed, _isPointsLessQuarter);
        _authorityImage.EnableInClassList(CommonUssClassNames.ImageAuthorityBlack, _isPointsLessZero);
        _authorityImage.EnableInClassList(CommonUssClassNames.ImageAuthorityGreen, _isPointsMoreZero);
        _authorityLabel.EnableInClassList(CommonUssClassNames.LabelAuthoritySizeSmall, _isPointsLong);
    }

    private void ValidateText()
    {
        if (_authority.Points > 99 || _authority.Points < -9)
            _isPointsLong = true;
        else
            _isPointsLong = false;
    }

    private void ValidatePoints()
    {
        if (_maxPoints <= _authority.Points)
        {
            _maxPoints = _authority.Points;
            PlayerPrefs.SetInt(_playerName + CommonSaveParameters.MaxPoints, _maxPoints);
            PlayerPrefs.Save();
        }

        if (_authority.Points > _maxPoints / 2)
        {
            _isPointsMoreZero = true;
            _isPointsLessZero = false;
            _isPointsLessQuarter = false;
            _isPointsLessHalf = false;
        }

        if (_authority.Points <= _maxPoints / 2)
        {
            _isPointsMoreZero = false;
            _isPointsLessZero = false;
            _isPointsLessQuarter = false;
            _isPointsLessHalf = true;
        }

        if (_authority.Points <= _maxPoints / 4)
        {
            _isPointsMoreZero = false;
            _isPointsLessZero = false;
            _isPointsLessQuarter = true;
            _isPointsLessHalf = false;
        }

        if (_authority.Points <= 0)
        {
            _isPointsMoreZero = false;
            _isPointsLessZero = true;
            _isPointsLessQuarter = false;
            _isPointsLessHalf = false;
        }
    }

    private void SetPointsIconsOpacityValue()
    {
        if (PlayerPrefs.HasKey(CommonSaveParameters.PointsIconsOpacity))
            _pointsIconsContainer.style.opacity =
                new StyleFloat(PlayerPrefs.GetFloat(CommonSaveParameters.PointsIconsOpacity));
        else
            _pointsIconsContainer.style.opacity = new StyleFloat(0.1f);
    }

    private void SavePlayerAuthority(string playerName, int authorityPoints)
    {
        PlayerPrefs.SetInt(playerName, authorityPoints);
        PlayerPrefs.Save();
    }
}