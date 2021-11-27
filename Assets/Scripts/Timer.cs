using UnityEngine;

public class Timer
{
    public bool beep = false;
    private bool m_pause = false;
    private float target_time;
    private float currenttime;

    public Timer(float targettime)
    {
        target_time = targettime;
    }
    public void starttimer()
    {
        currenttime = target_time;
        beep = false;
    }
    public void pause ()
    {
        m_pause = true;
    }
    public void unpause()
    {
        m_pause = false;
    }
    public void Update()
    {
        if (beep) return;
        if (m_pause) return;
        if (currenttime > 0)
        {
            currenttime -= Time.deltaTime;
            if (currenttime <= 0) beep = true;
        }
    }
}
