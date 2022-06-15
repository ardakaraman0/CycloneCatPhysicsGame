using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;

public class RigidBox : MonoBehaviour
{
    public double mass = 1;

    public double damping = 0.9;

    [SerializeField]
    bool freezeRotation = false;
    [SerializeField]
    Transform offset;
    [SerializeField]
    Shatter _shatter;


    private RigidBody m_body;
    private CollisionBox shape;

    void Start()
    {
        var pos = transform.position.ToVector3d();
        var scale = transform.localScale.ToVector3d() * 0.5;
        var rot = transform.rotation.ToQuaternion();

        m_body = new RigidBody();
        m_body.rigidBox = this;

        if (offset != null) m_body.offset = offset.position.y;
        else m_body.offset = 0;

        m_body.FreezeRotation = freezeRotation;
        m_body.Position = pos;
        m_body.Orientation = rot;
        m_body.LinearDamping = damping;
        m_body.AngularDamping = damping;
        m_body.SetMass(mass);
        m_body.SetAwake(true);
        m_body.SetCanSleep(true);

        shape = new CollisionBox(scale);
        shape.Body = m_body;
            

        RigidPhysicsEngine.Instance.Bodies.Add(m_body);
        RigidPhysicsEngine.Instance.Collisions.Primatives.Add(shape);
    }

    private void Update()
    {
        transform.position = m_body.Position.ToVector3();
        transform.rotation = m_body.Orientation.ToQuaternion();
    }

    public void Collision(RigidSphere rb)
    {
        if (!freezeRotation)
        {
            if (rb.CompareTag("Bullet"))
            {
                RigidPhysicsEngine.Instance.Bodies.Remove(m_body);
                RigidPhysicsEngine.Instance.Collisions.Primatives.Remove(shape);

                this.enabled = false;
                StartCoroutine(_shatter.SplitMesh(true));
            }
        }
    }

    public void CollisionGround()
    {
        if (!freezeRotation)
        {
            this.enabled = false;
            RigidPhysicsEngine.Instance.Bodies.Remove(m_body);
            RigidPhysicsEngine.Instance.Collisions.Primatives.Remove(shape);
            StartCoroutine(_shatter.SplitMesh(true));
        }
    }

    

}