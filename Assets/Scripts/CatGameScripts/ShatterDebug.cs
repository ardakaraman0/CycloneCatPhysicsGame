using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;


public class ShatterDebug : MonoBehaviour
{
    [SerializeField]
    Shatter _shatter;
    [SerializeField]
    RigidSphere sphere;
    

    public bool l = false;
    void Update()
    {
        if (l)
        {
            l = false;
            sphere.enabled = false;
            StartCoroutine(_shatter.SplitMesh(true));
        }
    }
}
