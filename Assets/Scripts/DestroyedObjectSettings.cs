using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyedObjectSettings", menuName = "SO/DestroyedObjects", order = 1)]
public class DestroyedObjectSettings : ScriptableObject
{
    public DestroyedObject prefab;          //prefab 
    public int m_health;                    //health
    public float Time_for_Destroy;          //time remaining befor deleting
    public float Time_for_Unvisual;
    public string m_tag;
    public int m_damage;                    //damage for all
    public int m_Score;
    public float min_velocity = 0.05f;      //speed difference for damage detection
    public AudioClip a_clip;                //Played from set damage

}
