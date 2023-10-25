using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MultiTouchMgr : MonoBehaviour
{
    public bool IsTouching {  get; private set; }
    public float minZoomInch = 2.54f;
    public float maxZoomInch = 2.54f;

    private float minZoomPixel;
    private float maxZoomPixel;

    private int primaryFingerId; 
    public float ZoomInch { get; private set; }
    private List<int> fingerIdList = new List<int>();

    private void Awake()
    {
        
    }

    public void UpdatePinchToZoom()
    {
        if (fingerIdList.Count >= 2)
        {
            Vector2[] prevTouchPos = new Vector2[2];
            Vector2[] currentTouchPos = new Vector2[2];
            for (int i = 0; i < 2; i++)
            {
                var touch = Array.Find(Input.touches, x => x.fingerId == fingerIdList[i]);

                currentTouchPos[i] = touch.position; //¿Œµ¶Ω∫∑Œ ¡¢±Ÿ«œ∏È æ»µ 
                prevTouchPos[i] = touch.position = touch.deltaPosition;
            }
            //PrevFrame Distance
            var prevFrameDist = Vector2.Distance(prevTouchPos[0], prevTouchPos[1]);
            //CurrFrame Distance
            var currFrameDist = Vector2.Distance(currentTouchPos[0], currentTouchPos[1]);

            var distancePixel = prevFrameDist - currFrameDist;
            var ZoomInch = distancePixel / Screen.dpi;
            Debug.Log(ZoomInch);
        }
    }

    public void Update()
    {

        foreach (var touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (fingerIdList.Count == 0 && primaryFingerId == int.MinValue)
                    {
                        primaryFingerId = touch.fingerId;
                    }
                    fingerIdList.Add(touch.fingerId);
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    if (primaryFingerId == touch.fingerId)
                    {
                        primaryFingerId = int.MinValue;
                    }
                    fingerIdList.Remove(touch.fingerId);
                    break;   
            }
        }

#if UNITY_EDITOR || UNITY_STANDALONE

#elif UNITY_ANDROID || UNITY_IOS
        UpdatePinchToZoom();
#endif



    }
}
