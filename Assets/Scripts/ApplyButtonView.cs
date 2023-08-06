using UnityEngine;
using UnityEngine.UIElements;

public class ApplyButtonView : MonoBehaviour
{
    private TournamentPlayerView[] _tournamentPlayerViews;
    private VisualElement _root;
    private CustomButton _applyButton;
    private int _counterSum;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _applyButton = _root.Q<CustomButton>("apply-button");
        _applyButton.AddToClassList(CommonUssClassNames.Hide);
        _tournamentPlayerViews = GetComponents<TournamentPlayerView>();
    }

    public void CheckDoneButton()
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
            _applyButton.AddToClassList(CommonUssClassNames.Hide);
        else
            _applyButton.RemoveFromClassList(CommonUssClassNames.Hide);
    }
}