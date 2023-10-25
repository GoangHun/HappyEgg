using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick2 : MonoBehaviour, IDragHandler
{
    public Vector2 Value { get; private set; }

    

    public void OnDrag(PointerEventData eventData)
    {
        Value = eventData.delta / Screen.dpi;
    }
}
