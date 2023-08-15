using UnityEngine;
using UnityEngine.UIElements;

public class ApplyButtonView : MonoBehaviour
{
    private int _counterSum;

    private TournamentPlayerView[] _tournamentPlayerViews;
    
    private VisualElement _root;
    private CustomButton _applyButton;
    private CustomButton _clearButton;

    private void Awake()
    {
        _tournamentPlayerViews = GetComponents<TournamentPlayerView>();

        _root = GetComponent<UIDocument>().rootVisualElement;

        _applyButton = _root.Q<CustomButton>("apply-button");
        _clearButton = _root.Q<CustomButton>("clear-button");

        _applyButton.AddToClassList(CommonUssClassNames.Hide);
        _clearButton.AddToClassList(CommonUssClassNames.Hide);
    }

    public void CheckPointsAmount()
    {
        _counterSum = 0;

        foreach (var tournamentPlayerView in _tournamentPlayerViews)
        {
            if (tournamentPlayerView.Counter == 0)
                _counterSum += 0;
            else
                _counterSum += 1;
        }

        if (_counterSum == 0)
        {
            _applyButton.AddToClassList(CommonUssClassNames.Hide);
            _clearButton.AddToClassList(CommonUssClassNames.Hide);
        }
        else
        {
            _applyButton.RemoveFromClassList(CommonUssClassNames.Hide);
            _clearButton.RemoveFromClassList(CommonUssClassNames.Hide);
        }
    }
}