using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    public GameObject ChildGo { get; set; } = null;
    public Pocket[] brothers = new Pocket[2];

    public bool Check(string tag)   //라인에 tag 개체가 이미 있는지 체크
    {
        foreach (var b in brothers)
        {
            if (b != null && b.CompareTag(tag))
                return false;
        }
        return true;
    }
}
