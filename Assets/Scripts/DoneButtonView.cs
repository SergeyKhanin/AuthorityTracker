using UnityEngine;
using UnityEngine.UIElements;

public class DoneButtonView : MonoBehaviour
{
    public TournamentPlayerView[] tournamentPlayerView;
    
    private VisualElement _root;
    private CustomButton _doneButton;
    
    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _doneButton = _root.Q<CustomButton>("done-button");
        _doneButton.AddToClassList(CommonUssClassNames.Hide);
    }

    public void CheckDoneButton()
    {
        if (tournamentPlayerView[0].Counter == 0 && tournamentPlayerView[1].Counter == 0)
            _doneButton.AddToClassList(CommonUssClassNames.Hide);
        else
            _doneButton.RemoveFromClassList(CommonUssClassNames.Hide);
    }
}