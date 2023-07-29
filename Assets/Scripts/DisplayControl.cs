using UnityEngine;

public class DisplayControl : MonoBehaviour
{
    void Start() => Screen.sleepTimeout = SleepTimeout.NeverSleep;
}