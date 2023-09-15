using Common;
using UnityEngine;
using UnityEngine.UIElements;

namespace View
{
    public class ToolsView : MonoBehaviour
    {
        private VisualElement _root;
        private VisualElement _toolsFrame;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _toolsFrame = _root.Q<VisualElement>("tools-frame");
        }

        private void Start() => SetToolsDirection();

        private void SetToolsDirection()
        {
            if (PlayerPrefs.HasKey(CommonSaveParameters.ToolsDirectionState))
            {
                var stringName = PlayerPrefs.GetString(CommonSaveParameters.ToolsDirectionState);
                var isNormal = stringName == CommonSaveParameters.ToolsDirectionNormal;

                _toolsFrame.EnableInClassList(CommonUssClassNames.ToolsSwap, isNormal);
            }
            else
                _toolsFrame.RemoveFromClassList(CommonUssClassNames.ToolsSwap);
        }
    }
}