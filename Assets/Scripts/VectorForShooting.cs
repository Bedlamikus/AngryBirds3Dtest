using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorForShooting : MonoBehaviour
{
    [SerializeField] private float m_step = 0.1f;
    [SerializeField] private float m_max_vert_angle = 60;
    [SerializeField] private float m_min_vert_angle = 10;
    [SerializeField] private float m_max_horiz_angle = 60;
    [SerializeField] private float m_min_horiz_angle = -60;
    [SerializeField] private GameObject RebornPoint;
    private float m_radius;

    private float a_x = 0;
    private float a_y = 0;
    private float alpha = 0;
    private float betta = 0;
    
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) a_x = +m_step;
        if (Input.GetKeyDown(KeyCode.DownArrow)) a_x = -m_step;
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) a_x = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) a_y = -m_step;
        if (Input.GetKeyDown(KeyCode.RightArrow)) a_y = m_step;
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) a_y = 0;

        alpha += a_x * Time.deltaTime;
        betta += a_y * Time.deltaTime;

        if (alpha > m_max_vert_angle) alpha = m_max_vert_angle;
        if (alpha < m_min_vert_angle) alpha = m_min_vert_angle;
        if (betta > m_max_horiz_angle) betta = m_max_horiz_angle;
        if (betta < m_min_horiz_angle) betta = m_min_horiz_angle;

        var n_y = Mathf.Sin(alpha * Mathf.PI / 180);
        var n_z = Mathf.Cos(alpha * Mathf.PI / 180) * Mathf.Cos(betta * Mathf.PI / 180);
        var n_x = Mathf.Cos(alpha * Mathf.PI / 180) * Mathf.Sin(betta * Mathf.PI / 180);

        transform.position = new Vector3(n_x, n_y, n_z)*m_radius+RebornPoint.transform.position;
    }

    public void SetRadius(float radius)
    {
        m_radius = radius;
    }
}
