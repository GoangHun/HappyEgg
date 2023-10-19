using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float timer = 0f;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (timer + 1f < Time.time)
        {
            timer = Time.time;
            float fps = 1.0f / Time.deltaTime;
            text.text = $"FPS: {fps:0.00}";
        }
        
    }

}
