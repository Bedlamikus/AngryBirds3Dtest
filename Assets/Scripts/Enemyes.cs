using System.Collections.Generic;
using UnityEngine;

// Этот скрипт служит контейнером для всех Enemy и предоставляет механизм подсчета оставшихся

public class Enemyes : MonoBehaviour
{
    [SerializeField] private List<DestroyedObject> m_enemyes;

    // Return count from list enemyes
    public int CountEnemyes()
    {
        if (m_enemyes == null) return 0;
        FindNull(0);
        return m_enemyes.Count;
    }

    public void AddListener()
    {
        if (m_enemyes != null)
        {
            for (int i = 0; i <= (m_enemyes.Count - 1); i++)
            {
                if (m_enemyes[i] != null)
                {
                    m_enemyes[i].death += FindNull;
                    m_enemyes[i].death += FindObjectOfType<ScoreController>().AddScore;
                }
            }
        }
    }

    // Find first and delete from list enemyes
    public void FindNull(int s)
    {
        if (m_enemyes !=null)
        {
            for (int i = 0; i <= (m_enemyes.Count - 1); i++)            
            {
                if (m_enemyes[i] == null)
                {
                    print(i);
                    m_enemyes.RemoveAt(i);
                    i--;
                }
            }
        }
    }
    private void Start()
    {
        AddListener();
    }
}
