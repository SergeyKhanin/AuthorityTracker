using UnityEngine;

namespace ScoringTokens
{
    public class Turntable : MonoBehaviour
    {

        void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 50f);
        }
    }
}
