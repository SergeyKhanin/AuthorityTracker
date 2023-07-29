using UnityEngine.UIElements;

public class CustomButton : Button
{
    public new class UxmlFactory : UxmlFactory<CustomButton, UxmlTraits>
    {
    }

    public CustomButton() => ClearClassList();
}