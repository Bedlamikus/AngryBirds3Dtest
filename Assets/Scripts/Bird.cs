using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Bird : DestroyedObject
{
    [SerializeField] private float m_mass = 1.0f;           //mass
    //[SerializeField] private float m_timeForDead = 2.0f;    //time remaining befor deleting
    [SerializeField] private float m_min_velocity = 0.02f;  //speed for deleting

    private float m_velocity = 0.0f;
    private bool m_fly = false;
    private bool m_dead = false;
    private Rigidbody m_rig;

    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        m_rig = GetComponent<Rigidbody>();
        m_rig.mass = m_mass;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (m_dead == false)
        {
            if (m_fly)
            {
                if (m_velocity <= m_min_velocity)
                    Dead();
            }
            else
            {
                if (m_velocity > 0) m_fly = true;
            }
            
        }
        m_velocity = m_rig.velocity.magnitude;
    }


    private void Dead()
    {
        m_dead = true;
        SetDamage(GetHealth()+1);
    }

}
