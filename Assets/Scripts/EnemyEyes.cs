using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyes : MonoBehaviour
{
    [SerializeField] private float m_max_y_angle = 20;
    [SerializeField] private float m_min_y_angle = -20;
    [SerializeField] private float intesiti = 1.0f;

    private Timer timer_y;
    // Start is called before the first frame update
    void Start()
    {
        timer_y = new Timer(intesiti);
        timer_y.starttimer();
    }

    // Update is called once per frame
    void Update()
    {
        timer_y.Update();
        if (timer_y.beep)
        {
            float r = Random.Range(m_min_y_angle, m_max_y_angle);
            var rot = new Vector3 (0, 0, r);
            transform.Rotate(rot);
            timer_y.starttimer();
        }
    }
}
