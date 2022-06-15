using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;


public class PlayerMechanics : MonoBehaviour
{
    float points;

    [SerializeField]
    RigidSphere attackSphere;
    [SerializeField]
    GameObject attackSphereObj;
    [SerializeField]
    BulletMechanic _bullet;

    [SerializeField]
    float _bulletLifetime;

    [SerializeField]
    Transform attackPos;
    [SerializeField]
    float power = 5f;

    public void Attack(float t)
    {
        _bullet = Spawner.Instance.Get();
        _bullet.SetAndFire(attackPos.position.ToVector3d(), attackPos.forward.ToVector3d(), _bulletLifetime, power);
    }


}
