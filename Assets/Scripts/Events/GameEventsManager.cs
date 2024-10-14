using UnityEngine.Events;

namespace Events
{
    public static class GameEventsManager
    {
        public static readonly UnityEvent ApplyPoints = new();
        public static readonly UnityEvent ClearPoints = new();
    }
}
