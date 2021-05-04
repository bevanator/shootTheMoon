using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowFactor = 0.5f;
    public float turboFactor = 1.2f;

    public void SlowMo()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void turbo()
    {
        Time.timeScale = turboFactor;
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ResetTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void FreezeTime()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
