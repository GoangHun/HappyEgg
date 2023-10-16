using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    public GameObject ChildGo { get; set; } = null;
    public Pocket[] brothers = new Pocket[2];

    public bool Check(string tag)   //���ο� tag ��ü�� �̹� �ִ��� üũ
    {
        foreach (var b in brothers)
        {
            if (b != null && b.CompareTag(tag))
                return false;
        }
        return true;
    }
}
