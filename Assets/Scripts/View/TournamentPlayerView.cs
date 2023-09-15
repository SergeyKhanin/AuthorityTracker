using Common;
using Core;
using Elements;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
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
        private VisualElement _pointsIconsContainer;
        private CustomButton _minusButton;
        private CustomButton _plusButton;
        private CustomButton _applyButton;
        private CustomButton _clearButton;
        private CustomButton _plus5Button;
        private CustomButton _minus10Button;
        private CustomLabel _authorityLabel;
        private CustomLabel _pointsLabel;
        private CustomLabel _pointsMinusLabel;

        private string _playerName;
        private int _startPoints;
        private int _maxPoints;
        private bool _isPlus5ButtonClicked;
        private bool _isMinus10ButtonClicked;
        private bool _isPointsLong;
        private bool _isPointsLessZero;
        private bool _isPointsMoreZero;
        private bool _isPointsLessHalf;
        private bool _isPointsLessQuarter;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _applyButtonView = GetComponent<ApplyButtonView>();
            _playerName = SetPlayers.GetPlayerFromList(playersList);
            _authority = new Authority();

            _frame = _root.Q<VisualElement>(_playerName);

            _authorityImage = _frame.Q<VisualElement>("authority-image");
            _pointsIconsContainer = _frame.Q<VisualElement>("points-icons-container");
            _iconPlus = _frame.Q<VisualElement>("icon-plus");
            _iconMinus = _frame.Q<VisualElement>("icon-minus");

            _minusButton = _frame.Q<CustomButton>("minus-button");
            _plusButton = _frame.Q<CustomButton>("plus-button");
            _applyButton = _root.Q<CustomButton>("apply-button");
            _clearButton = _root.Q<CustomButton>("clear-button");
            _plus5Button = _frame.Q<CustomButton>("plus-5-button");
            _minus10Button = _frame.Q<CustomButton>("minus-10-button");

            _authorityLabel = _frame.Q<CustomLabel>("authority-label");
            _pointsLabel = _frame.Q<CustomLabel>("points-label");

            _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
            _iconPlus.AddToClassList(CommonUssClassNames.Hide);
            _iconMinus.AddToClassList(CommonUssClassNames.Hide);

            if (PlayerPrefs.HasKey(_playerName))
                _authority.SetPoints(PlayerPrefs.GetInt(_playerName));

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

        private void OnEnable()
        {
            _plusButton.clicked += OnPlusButtonClicked;
            _minusButton.clicked += OnMinusButtonClicked;
            _plus5Button.clicked += OnPlus5ButtonClicked;
            _minus10Button.clicked += OnMinus10ButtonClicked;
            _applyButton.clicked += OnApplyButtonClicked;
            _clearButton.clicked += OnClearButtonClicked;
        }

        private void OnDisable()
        {
            _plusButton.clicked -= OnPlusButtonClicked;
            _minusButton.clicked -= OnMinusButtonClicked;
            _plus5Button.clicked -= OnPlus5ButtonClicked;
            _minus10Button.clicked -= OnMinus10ButtonClicked;
            _applyButton.clicked -= OnApplyButtonClicked;
            _clearButton.clicked -= OnClearButtonClicked;
        }

        private void OnPlusButtonClicked()
        {
            if (_isPlus5ButtonClicked)
                Counter += 5;
            else
                Counter++;

            _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
            _iconPlus.RemoveFromClassList(CommonUssClassNames.Hide);
            _applyButtonView.CheckPointsAmount();

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
            _applyButtonView.CheckPointsAmount();

            UpdatePointsLabel();

            _isMinus10ButtonClicked = false;
        }

        private void OnApplyButtonClicked()
        {
            _authority.AddCustomPoints(Counter);

            ValidatePoints();
            ValidateText();
            ValidateClasses();
            SavePlayerAuthority(_playerName, _authority.Points);

            Counter = 0;

            _authorityLabel.text = _authority.Points.ToString();
            _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
            _iconPlus.AddToClassList(CommonUssClassNames.Hide);
            _iconMinus.AddToClassList(CommonUssClassNames.Hide);

            _applyButtonView.CheckPointsAmount();
        }

        private void OnClearButtonClicked()
        {
            Counter = 0;

            _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
            _iconPlus.AddToClassList(CommonUssClassNames.Hide);
            _iconMinus.AddToClassList(CommonUssClassNames.Hide);

            _applyButtonView.CheckPointsAmount();
        }

        private void UpdatePointsLabel()
        {
            if (Counter < 0)
            {
                _pointsLabel.text = Counter.ToString().TrimStart('-');
                _pointsLabel.RemoveFromClassList(CommonUssClassNames.PointsLabelGreen);
                _pointsLabel.AddToClassList(CommonUssClassNames.PointsLabelRed);
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
                _pointsLabel.RemoveFromClassList(CommonUssClassNames.PointsLabelRed);
                _pointsLabel.AddToClassList(CommonUssClassNames.PointsLabelGreen);
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
                _pointsIconsContainer.style.opacity = new StyleFloat(PlayerPrefs.GetFloat(CommonSaveParameters.PointsIconsOpacity));
            else
                _pointsIconsContainer.style.opacity = new StyleFloat(0.1f);
        }

        private void SavePlayerAuthority(string playerName, int authorityPoints)
        {
            PlayerPrefs.SetInt(playerName, authorityPoints);
            PlayerPrefs.Save();
        }
    }
}