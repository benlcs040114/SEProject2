using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF_destroy : MonoBehaviour
{

    public float DestroyTime;//Destroy time after enemy dies
    void Start()
    {
        Destroy(this.gameObject, DestroyTime);
    }


}
