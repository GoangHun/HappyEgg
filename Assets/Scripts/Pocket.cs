using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    public GameObject ChildGo { get; set; } = null;
    public Pocket[] brothers = new Pocket[2];

    public bool Check()
    {
        return brothers[0].ChildGo == null || brothers[1].ChildGo == null;
    }
}
