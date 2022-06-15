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
    [SerializeField]
    RigidBox box;

    public bool l = false;
    void Update()
    {
        if (l)
        {
            l = false;
            if (sphere != null) sphere.enabled = false;
            else box.enabled = false;

            StartCoroutine(_shatter.SplitMesh(true));
        }
    }
}
