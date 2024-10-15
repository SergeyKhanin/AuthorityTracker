using Common;
using UnityEngine;

namespace Player
{
    public sealed class PlayerModel
    {
        public string Name { get; private set; } = string.Empty;
        public int Points { get; private set; } = 50;
        public int Counter { get; private set; }

        public void SetName(string name) => Name = name;

        public void SetPoints(int points) => Points = points;

        public void X1Plus() => Counter += 1;

        public void X5Plus() => Counter += 5;

        public void X1Minus() => Counter -= 1;

        public void X5Minus() => Counter -= 5;

        public void Apply()
        {
            Points += Counter;
            Counter = 0;
            SavePoints(Points);
        }

        public void Clear() => Counter = 0;

        private void SavePoints(int points)
        {
            PlayerPrefs.SetInt(Name, points);
            PlayerPrefs.Save();
        }
    }
}
