public class Authority
{
    public int Points { get; set; } = 50;
    public readonly int Limit = 999;

    public void PlusPoint() => Points++;

    public void MinusPoint() => Points--;

    public void PlusFivePoints() => Points += 5;

    public void MinusFivePoints() => Points -= 5;

    public void AddCustomPoints(int amount) => Points += amount;

    public void ValidatePoints()
    {
        if (Points >= Limit)
            Points = Limit;
        if (Points <= -Limit)
            Points = -Limit;
    }
}