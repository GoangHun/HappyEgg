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
            //�� �����Ӹ��� 1�� ������Ŵ
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
        // 6�ڸ��� ���缭 �ؽ�Ʈ ������Ʈ
        m_TextMeshPro.text = currentScore.ToString("D6");
    }
}
