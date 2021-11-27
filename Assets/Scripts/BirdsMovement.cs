using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdsMovement : MonoBehaviour
{
    [SerializeField] private float m_min_force = 2.0f;          //minimal force for shoot
    [SerializeField] private float m_max_force = 10.0f;         //maximal force for shoot
    [SerializeField] private float m_force_stick = 0.1f;        //При задержке Spacebar на эту величину будет прирост Force
    [SerializeField] private Bird prefab_Bird;                  // prefab Bird
    [SerializeField] private List<Bird> m_birds;                //All birds for shooting
    [SerializeField] private AudioClip a_shooting;
    [SerializeField] private AudioClip a_load_bird;
    [SerializeField] private float m_radius = 0.5f;
    [SerializeField] private float m_min_radius = 0.5f;
    private AudioSource audio_player;


    private Transform target_Bird_Transform;                    //this transform for Camera movement and infinity logic
    [SerializeField] private Transform PositionForRebornBirds;  //Space for reborn bird
    [SerializeField] private VectorForShooting Aim;                     //vector for shoot
    [SerializeField] private Rigidbody m_rig;                     
    [SerializeField] private float m_force = 2.0f;
    private bool shooting = false;
    private bool addingForce = false;
    private bool infinity = false;
    [SerializeField] Button button_infinity;
    [SerializeField] Sprite Ex_Infinity;
    [SerializeField] Sprite Infinity;
    private bool s_state_fly = false;

    // Start is called before the first frame update
    private void Start()
    {
        AddBird();
        AddBird();
        AddBird();
        if (button_infinity) button_infinity.image.sprite = Ex_Infinity;    //Set infinity sprite at start
        audio_player = GetComponent<AudioSource>();
        Aim.SetRadius(m_min_radius);
        s_state_fly = false;
    }
    private void Update()
    {
        if (shooting == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                addingForce = true;
                audio_player.clip = a_load_bird;
                audio_player.Play();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Shoot();
                shooting = true;
                audio_player.Stop();
                audio_player.clip = a_shooting;
                audio_player.Play();
                s_state_fly = true;
            }

            if (addingForce) AddForce();
            if (target_Bird_Transform)
            {
                float tx = 2*PositionForRebornBirds.transform.position.x - Aim.transform.position.x;
                float ty = 2*PositionForRebornBirds.transform.position.y - Aim.transform.position.y;
                float tz = 2*PositionForRebornBirds.transform.position.z - Aim.transform.position.z;
                target_Bird_Transform.position = new Vector3(tx, ty, tz);
            }
            s_state_fly = false;
        }
        else //Если мы выстрелили, но хотим запустить вторую птичку то снова нажимаем пробел и заряжаем новую
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                target_Bird_Transform = null;
                s_state_fly = false;
            }
        }
        if (target_Bird_Transform == null && m_birds.Count > 0)
        {
            SetNewBirdAtPosition();
            shooting = false;
            s_state_fly = false;
        }
    }

    public bool IsFlyBird()
    {
        return s_state_fly;
    }

    private Vector3 GetShootVector()
    {
        return (Aim.transform.position - PositionForRebornBirds.transform.position).normalized;
    }
    //Shoot bird in sky
    private void Shoot()
    {
        if (m_rig)
        {
            m_rig.useGravity = true;                                             //
            m_rig.AddForce( GetShootVector() * m_force, ForceMode.Force);    //
        }

        m_force = m_min_force;                                                  //reset m_force
        addingForce = false;                                                    //reset flag for increment force
        var s = m_min_radius + m_force * m_radius / m_max_force;
        Aim.SetRadius(s);
    }

    // increment m_force for Shoot
    private void AddForce()
    {
        m_force += m_force_stick*Time.deltaTime;                           
        if (m_force >= m_max_force) m_force = m_max_force;
        var s = m_min_radius+ m_force * m_radius / m_max_force;
        Aim.SetRadius(s);
    }

    //Get transform position from bird in fly or target position for reborn
    public Vector3 GetTransformPosition()
    {
        if (target_Bird_Transform) return target_Bird_Transform.position;
        else return PositionForRebornBirds.position;
    }

    public Transform GetTargetTransform()
    {
        if (target_Bird_Transform) return target_Bird_Transform;
        else return PositionForRebornBirds;
    }

    //Adding Bird in list
    public void AddBird()
    {
        Bird b = Instantiate < Bird >( prefab_Bird);            //Create Bird from prefab
        b.transform.position = PositionForRebornBirds.position; //Set Bird position 
        b.gameObject.SetActive(false);                          //Hide Bird
        m_birds.Add(b);                                         //Add Bird in list with all birds
    }

    //Reload ShootBoard weapon
    private void SetNewBirdAtPosition()
    {
        if (m_birds == null || m_birds[0] == null) return;
        target_Bird_Transform = m_birds[0].transform;           //set transform 
        m_birds[0].gameObject.SetActive(true);                  //Show new bird
        m_rig = m_birds[0].GetComponent<Rigidbody>();
        m_rig.drag = 0.3f;
        m_birds.RemoveAt(0);                                    //remove from list
        if (infinity)
            AddBird();
    }
    public void Set_Infinity()
    {
        if (infinity)
        {
            button_infinity.image.sprite = Ex_Infinity;
            infinity = false;
        }
        else
        {
            button_infinity.image.sprite = Infinity;
            infinity = true;
            
            //Если хотим бесконечные птички но они закончились, то добавляем одну
            if (m_birds.Count == 0)
                AddBird();
        }

        //deselect button from Unity manual
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
