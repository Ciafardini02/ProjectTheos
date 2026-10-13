using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    
    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }
}
