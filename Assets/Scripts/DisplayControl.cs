using UnityEngine;

public class DisplayControl : MonoBehaviour
{
    [SerializeField] private bool hasSleepTimeout;

    void Start()
    {
        if (hasSleepTimeout)
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        else
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
    }
}