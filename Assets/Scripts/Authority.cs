public class Authority
{
    public int Points { get; private set; }
    public int Limit { get; }

    public Authority()
    {
        Points = 50;
        Limit = 999;
    }

    public void PlusPoint() => Points++;
    public void MinusPoint() => Points--;
    public void PlusFivePoints() => Points += 5;
    public void MinusFivePoints() => Points -= 5;
    public void AddCustomPoints(int amount) => Points += amount;
    public void SetPoints(int amount) => Points = amount;

    public void ValidatePoints()
    {
        if (Points >= Limit)
            Points = Limit;
        if (Points <= -Limit)
            Points = -Limit;
    }
}