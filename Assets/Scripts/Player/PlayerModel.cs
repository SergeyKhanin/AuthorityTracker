namespace Player
{
    public sealed class PlayerModel
    {
        public int Points { get; private set; }

        public void X1Plus() => Points += 1;

        public void X5Plus() => Points += 5;

        public void X1Minus() => Points -= 1;

        public void X5Minus() => Points -= 5;
    }
}
