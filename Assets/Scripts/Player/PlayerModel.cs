namespace Player
{
    public sealed class PlayerModel
    {
        public int Points { get; private set; }
        public int Counter { get; private set; }

        public void X1Plus() => Counter += 1;

        public void X5Plus() => Counter += 5;

        public void X1Minus() => Counter -= 1;

        public void X5Minus() => Counter -= 5;

        public void Apply() => Points += Counter;

        public void Clear() => Counter = 0;
    }
}
