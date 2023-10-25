using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Update is called once per frame
    void Update()
    {
        var message = string.Empty;

        foreach (Touch touch in Input.touches)
        {
            message += "Touch ID: " + touch.fingerId + "\tPhase" + touch.phase + "\n";
            message += "\tPhase" + touch.phase;
            message += "\tPosition: " + touch.position;
            message += "\tDelta pos; " + touch.deltaPosition + "\n";
            message += "\tDelta time; " + touch.deltaTime + "\n";
            
        }
        message += "\n";
        text.text = message;

    }
}
