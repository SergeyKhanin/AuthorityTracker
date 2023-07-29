using UnityEngine.UIElements;

public class CustomLabel : Button
{
    public new class UxmlFactory : UxmlFactory<CustomLabel, UxmlTraits>
    {
    }

    public CustomLabel() => ClearClassList();
}