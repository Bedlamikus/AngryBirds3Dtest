using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private ScoreUI m_scoreUI;
    private int m_Score;
    // Start is called before the first frame update
    void Start()
    {
        m_Score = 0;
        m_scoreUI.SetScore(m_Score);
    }

    public void AddScore(int score)
    {
        m_Score += score;
        m_scoreUI.SetScore(m_Score);
    }

    public int GetSvore()
    {
        return m_Score;
    }

}
