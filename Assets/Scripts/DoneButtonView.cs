using UnityEngine;
using UnityEngine.UIElements;

public class DoneButtonView : MonoBehaviour
{
    private TournamentPlayerView[] _tournamentPlayerViews;
    private VisualElement _root;
    private CustomButton _doneButton;
    private int _counterSum;
    private bool _isTournamentPlayerViewsEmpty;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _doneButton = _root.Q<CustomButton>("done-button");
        _doneButton.AddToClassList(CommonUssClassNames.Hide);
        _tournamentPlayerViews = GetComponents<TournamentPlayerView>();

        _isTournamentPlayerViewsEmpty = _tournamentPlayerViews.Length == 0;

        if (_isTournamentPlayerViewsEmpty)
            Debug.LogWarning($"Don't have <color=red>{_tournamentPlayerViews.GetType().Name}</color> component");
    }

    public void CheckDoneButton()
    {
        if (_isTournamentPlayerViewsEmpty) return;

        _counterSum = 0;

        foreach (var tournamentPlayerView in _tournamentPlayerViews)
        {
            if (tournamentPlayerView.Counter == 0)
                _counterSum += 0;
            else
                _counterSum += 1;
        }

        if (_counterSum == 0)
            _doneButton.AddToClassList(CommonUssClassNames.Hide);
        else
            _doneButton.RemoveFromClassList(CommonUssClassNames.Hide);
    }
}