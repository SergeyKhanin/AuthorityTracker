using UnityEngine;

public class Authority
{
    public int Points { get; private set; }
    public int Limit { get; }

    public Authority()
    {
        if (PlayerPrefs.HasKey("InitialPoints"))
            Points = PlayerPrefs.GetInt("InitialPoints");
        else
            Points = 50;

        Limit = 999;
    }

    public void PlusPoint() => Points++;

    public void MinusPoint() => Points--;

    public void PlusFivePoints() => Points += 5;

    public void MinusFivePoints() => Points -= 5;

    public void ValidatePoints()
    {
        if (Points >= Limit)
            Points = Limit;
        if (Points <= -Limit)
            Points = -Limit;
    }
}