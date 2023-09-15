using UnityEngine.UIElements;

namespace Elements
{
    public class CustomLabel : Button
    {
        public new class UxmlFactory : UxmlFactory<CustomLabel, UxmlTraits>
        {
        }

        public CustomLabel() => ClearClassList();
    }
}