using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyedObject : MonoBehaviour
{
    [SerializeField] private DestroyedObjectSettings settings;

    [SerializeField] private int m_health;                  //health
    [SerializeField] private float Time_for_Destroy;        //time remaining befor deleting
    [SerializeField] private float Time_for_Unvisual;
    [SerializeField] private string m_tag;
    [SerializeField] private int m_damage;                  //damage for all
    [SerializeField] private int m_Score;
    [SerializeField] private float min_velocity = 0.05f;    //speed difference for damage detection
    [SerializeField] private AudioClip a_clip;

    private AudioSource a_source;
    protected float timer = 0;                              //timer for deleting
    [SerializeField] private bool Destroy_This = false;     //if that is true then destroy "this"
    [SerializeField] protected Rigidbody Rigidbody_visual;  //ссылка на физику визуальной части
    [SerializeField] protected GameObject Visual_object;    //¬изуальна€ составл€юща€ объекта
    private Collider m_collider;

    public delegate void Death(int score);
    public event Death death;

    //Timers
    private Timer Destroy_timer;
    private Timer UnVisual_timer;
    private Timer Delay_for_Change_Tag; //2 sec

    protected virtual void Start()
    {
        a_source = GetComponent<AudioSource>();
        m_collider = GetComponent<Collider>();
        Destroy_timer = new Timer(Time_for_Destroy);
        UnVisual_timer = new Timer(Time_for_Unvisual);
        Delay_for_Change_Tag = new Timer(2.0f);
        Delay_for_Change_Tag.starttimer();
    }

    protected virtual void Play_Death_Sound()
    {
        AudioPlayer.singleton.Play_Bird_death(0);
    }

    protected virtual void Update()
    {
        //Timers tick
        Destroy_timer.Update();
        UnVisual_timer.Update();
        Delay_for_Change_Tag.Update();
        if (gameObject.tag != "Destroyed" && Delay_for_Change_Tag.beep)
            gameObject.tag = "Destroyed";

        //Logic 
        if (Destroy_This)
        {
            if (Visual_object !=null && UnVisual_timer.beep) 
            {
                Destroy(Visual_object);
                m_collider.enabled = false;
                death?.Invoke(m_Score);
            }
            if (Destroy_timer.beep)
            {

                Destroy(gameObject);
                return;
            }
        }
    }

    protected void StartDestroy()
    {
        Destroy_timer.starttimer();
        UnVisual_timer.starttimer();
        Destroy_This = true;
    }

    public void SetDamage(int damage)
    {
        if (a_source) a_source.PlayOneShot(a_clip);
        m_health -= damage;
        if (m_health <= 0)
        {
            StartDestroy();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Destroyed")
        {
            DestroyedObject enemy = collision.gameObject.GetComponent<DestroyedObject>();
            if (GetVelocity() - enemy.GetVelocity() > min_velocity)
            {
                enemy.SetDamage(m_damage);
                SetDamage(enemy.GetDamage());
            }
        }
    }

    public int GetDamage()
    {
        return m_damage;
    }

    public float GetVelocity()
    {
        return Rigidbody_visual.velocity.magnitude;
    }
    public int GetHealth()
    {
        return m_health;
    }

    public void Enabled_Tag()
    {
        gameObject.tag = m_tag;
    }
}
