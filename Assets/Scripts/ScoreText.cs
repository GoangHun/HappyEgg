using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private float activeTime;
    private int currentScore = 0;
    private TextMeshProUGUI m_TextMeshPro;

    private void Awake()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateScoreText();
        activeTime = Time.unscaledTime;
    }

    void Update()
    {
        if (GameManager.Instance.IsGameover)
        {
            if (currentScore < GameManager.Instance.Score)
            {
                if (Time.unscaledTime > activeTime + 1f && Input.anyKey)
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
        m_TextMeshPro.text = $"{currentScore:#,###}";
	}
}
