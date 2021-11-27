using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private BirdsMovement birds_Movement;
    [SerializeField] private float m_Offset_z = 3;
    [SerializeField] private float m_Offset_y = 3;
    [SerializeField] private float m_Offset_z_fly = 3;
    [SerializeField] private float m_Offset_y_fly = 3;
    [SerializeField] private float m_Left_Wall;
    [SerializeField] private float m_Right_Wall;
    [SerializeField] private float m_Bottom;
    [SerializeField] private float m_top;
    [SerializeField] private Transform StopLine;
    [SerializeField] private Transform StopPoint;
    private bool m_stop_mooving = false;
    private float ofs_y;
    private float ofs_z;
    private Transform m_transform;
    private Quaternion deltaRotation;
    //private Transform target;

    void Update()
    {
        m_transform = birds_Movement.GetTargetTransform();
        if (m_transform == null) return;
        var m_rig = m_transform.GetComponent<Rigidbody>();
        Vector3 m_rig_vel;
        if (m_rig == null)
        {
            m_rig_vel = new Vector3(0, 0, 1);
        }
        else
        {
            m_rig_vel = m_rig.velocity.normalized;
        }
        if (m_transform == null) return;

        var nx = m_transform.position;

        if (nx.x <= m_Left_Wall) nx.x= m_Left_Wall;
        if (nx.x >= m_Right_Wall) nx.x = m_Right_Wall;
        if (nx.y <= m_Bottom) nx.y = m_Bottom;
        if (nx.y >= m_top) nx.y = m_top;

        ofs_y = m_Offset_y;
        ofs_z = m_Offset_z;
        if (birds_Movement.IsFlyBird())
        {
            ofs_y = m_Offset_y_fly;
            ofs_z = m_Offset_z_fly;
        }
        else m_stop_mooving = false;
        if (nx.z >= StopLine.position.z) m_stop_mooving = true;
        if (m_stop_mooving)
        {
            nx = StopPoint.position;
        }
        transform.Translate((nx-transform.position) * Time.deltaTime);
        cam.transform.localPosition = new Vector3(0,ofs_y,ofs_z);
    }
}
