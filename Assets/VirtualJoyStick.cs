using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum Axis
    {
        Horizontal,
        Vertical,
    }

    public Image stick;
    public float radius;

    private Vector3 originalPoint;
    private RectTransform rectTr;

    private Vector2 value;

    private int pointerId;
    private bool isDragging = false;

    private void Start()
    {
        rectTr = GetComponent<RectTransform>();
        originalPoint = stick.rectTransform.position;    //현재 앵커에 맞는 포지션
    }

    public float GetAxis(Axis axis)
    {
        switch (axis)
        {
            case Axis.Horizontal:
                return value.x;

            case Axis.Vertical:
                return value.y;
        }
        return 0f;
    }

    private void Update()
    {
        Debug.Log($"{GetAxis(Axis.Horizontal)} / {GetAxis(Axis.Vertical)}");
    }

    public void OnDrag(PointerEventData eventData)  //eventData.position은 스크린 좌표계의 포지션
    {
        if (pointerId != eventData.pointerId)
            return;
        UpdateStick(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging)
            return;

        isDragging = true;
        pointerId = eventData.pointerId;
        UpdateStick(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        isDragging = false;
        stick.rectTransform.position = originalPoint;
        value = Vector2.zero;
    }

    public void UpdateStick(Vector3 screenPos)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTr, screenPos, null, out Vector3 newPoint);
        var delta = Vector3.ClampMagnitude(newPoint - originalPoint, radius);
        value = delta.normalized;

        stick.rectTransform.position = originalPoint + delta;
    }

   
}
