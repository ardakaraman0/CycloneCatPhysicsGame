using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;


public class BulletMechanic : MonoBehaviour
{
    [SerializeField]
    RigidSphere _rigidSphere;

    public AttackType _attackType;
    
    public void SetAndFire(Vector3d initial, Vector3d direction, float t, float power, AttackType attackType)
    {
        if (attackType == AttackType.bullet) gameObject.tag = "Bullet";
        else gameObject.tag = "Paw";

        _rigidSphere.SetInitial(initial, direction, t, power);
    }


}
