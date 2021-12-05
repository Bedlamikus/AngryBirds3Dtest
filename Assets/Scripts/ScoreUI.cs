using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text m_score;
    public void SetScore(int score)
    {
        m_score.text = score.ToString();
    }
}
