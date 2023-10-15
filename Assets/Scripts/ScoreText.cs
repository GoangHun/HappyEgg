using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private int currentScore = 0;
    private TextMeshProUGUI m_TextMeshPro;

    private void Awake()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateScoreText();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameover)
        {
            //매 프레임마다 1씩 증가시킴
            if (currentScore < GameManager.Instance.Score)
            {
                if (Input.anyKey)
                {
                    currentScore = GameManager.Instance.Score;
                }
                else
                {
                    currentScore += 100;
                }
                UpdateScoreText();
            }
        }
    }

    void UpdateScoreText()
    {
        // 6자리로 맞춰서 텍스트 업데이트
        m_TextMeshPro.text = currentScore.ToString("D6");
    }
}
