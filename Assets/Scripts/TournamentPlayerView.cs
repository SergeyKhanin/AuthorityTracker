using System;
using UnityEngine;
using UnityEngine.UIElements;

public class TournamentPlayerView : MonoBehaviour
{
    [SerializeField] private PlayersRoster.PlayersList playersRoster;
    [SerializeField] private TournamentPlayerView test;

    private Authority _authority;
    private VisualElement _root;
    private VisualElement _frame;
    private string _playerName;
    private CustomButton _minusButton;
    private CustomButton _plusButton;
    private CustomButton _doneButton;
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
        _doneButton = _root.Q<CustomButton>("done-button");

        _authorityLabel = _frame.Q<CustomLabel>("authority-label");
        _pointsLabel = _frame.Q<CustomLabel>("points-label");
        
        _doneButton.AddToClassList(CommonUssClassNames.Hide);
        _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        _authorityLabel.text = _authority.Points.ToString();
    }

    private void Update()
    {
        if (_counter == 0 && test._counter == 0)
            _doneButton.AddToClassList(CommonUssClassNames.Hide);
            
        else
            _doneButton.RemoveFromClassList(CommonUssClassNames.Hide);
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
        _counter++;
        _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
        SomeMethod();

    }

    
    private void OnMinusButtonClicked()
    {
        _counter--;
        _pointsLabel.RemoveFromClassList(CommonUssClassNames.Hide);
        SomeMethod();
    }

    private void OnDoneButtonClicked()
    {
        _authority.Points += _counter;
        _counter = 0;
        _authorityLabel.text = _authority.Points.ToString();
        _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        _doneButton.AddToClassList(CommonUssClassNames.Hide);
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
    
    private void SomeMethod()
    {
        if (_counter < 0)
        {
            _pointsLabel.text = _counter.ToString().TrimStart('-');
            _pointsLabel.AddToClassList(CommonUssClassNames.PointsLabelMinus);
        }

        else if(_counter == 0)
        {
            _pointsLabel.AddToClassList(CommonUssClassNames.Hide);
        }
            
        else
        {
            _pointsLabel.text = _counter.ToString();
            _pointsLabel.RemoveFromClassList(CommonUssClassNames.PointsLabelMinus);
            _pointsLabel.AddToClassList(CommonUssClassNames.PointsLabelPlus);
        }
    }
}