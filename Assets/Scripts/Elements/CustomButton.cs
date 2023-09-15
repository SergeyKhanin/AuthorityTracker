using UnityEngine.UIElements;

namespace Elements
{
    public class CustomButton : Button
    {
        public new class UxmlFactory : UxmlFactory<CustomButton, UxmlTraits>
        {
        }

        public CustomButton() => ClearClassList();
    }
}