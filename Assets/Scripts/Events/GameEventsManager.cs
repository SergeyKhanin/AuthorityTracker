using UnityEngine.Events;

namespace Events
{
    public static class GameEventsManager
    {
        public static readonly UnityEvent PointsApplied = new();
        public static readonly UnityEvent PointsCleared = new();
        public static readonly UnityEvent CounterChanged = new();
    }
}
