using UnityEngine.Events;

namespace Events
{
    public static class EventsManager
    {
        public static readonly UnityEvent PointsApplied = new();
        public static readonly UnityEvent PointsCleared = new();
        public static readonly UnityEvent CounterChanged = new();
        public static readonly UnityEvent PointsRestarted = new();
        public static readonly UnityEvent PauseOpened = new();
        public static readonly UnityEvent PauseClosed = new();
        public static readonly UnityEvent SettingsOpened = new();
        public static readonly UnityEvent SettingsClosed = new();
    }
}
