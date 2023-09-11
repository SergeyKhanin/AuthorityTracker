using Common;
using UnityEngine;

public class Authority
{
    private readonly int _defaultValue = 50;
    private readonly int _limit = 999;
    public int Points { get; private set; }

    public Authority()
    {
        if (PlayerPrefs.HasKey(CommonSaveParameters.InitialPoints))
            Points = PlayerPrefs.GetInt(CommonSaveParameters.InitialPoints);
        else
        {
            PlayerPrefs.SetInt(CommonSaveParameters.InitialPoints, _defaultValue);
            Points = _defaultValue;
        }
    }

    public void PlusPoint()
    {
        Points++;
        ValidatePoints();
    }

    public void MinusPoint()
    {
        Points--;
        ValidatePoints();
    }

    public void PlusFivePoints()
    {
        Points += 5;
        ValidatePoints();
    }

    public void MinusFivePoints()
    {
        Points -= 5;
        ValidatePoints();
    }

    public void AddCustomPoints(int amount)
    {
        Points += amount;
        ValidatePoints();
    }

    public void SetPoints(int amount)
    {
        Points = amount;
        ValidatePoints();
    }

    private void ValidatePoints()
    {
        if (Points >= _limit)
            Points = _limit;
        if (Points <= -_limit)
            Points = -_limit;
    }
}