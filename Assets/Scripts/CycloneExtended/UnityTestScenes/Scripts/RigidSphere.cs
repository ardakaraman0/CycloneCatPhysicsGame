using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;
using Quaternion = Cyclone.Core.Quaternion;

public class RigidSphere : MonoBehaviour
{
    public double mass = 1;

    public double damping = 0.9;

    private RigidBody m_body;

    [SerializeField]
    bool useGravity = true;

    bool isActive = false;

    [SerializeField]
    BulletMechanic _bulletMechanic;

    double scale;
    Quaternion rot;
    CollisionSphere shape;
    Vector3d pos;

    private void Awake()
    {
        m_body = new RigidBody();
    }
    
    public bool shoot = false;

    void Start()
    {
        pos = transform.position.ToVector3d();
        scale = transform.localScale.y * 0.5;
        rot = transform.rotation.ToQuaternion();


        m_body.rigidSphere = this;

        m_body.Position = pos;
        m_body.Orientation = rot;
        m_body.LinearDamping = damping;
        m_body.AngularDamping = damping;

       
        m_body.SetMass(mass);
        m_body.SetAwake(true);
        m_body.SetCanSleep(true);

        shape = new CollisionSphere(scale);
        shape.Body = m_body;

        RigidPhysicsEngine.Instance.Bodies.Add(m_body);
        RigidPhysicsEngine.Instance.Collisions.Primatives.Add(shape);
        
    }

    private void Update()
    {
        transform.position = m_body.Position.ToVector3();
        transform.rotation = m_body.Orientation.ToQuaternion();

        if (shoot)
        {
            shoot = false;
            m_body.AddForce(Vector3d.UnitZ * 100f);
            m_body.Integrate(Time.deltaTime);   
        }
    }

    public void SetInitial(Vector3d initial, Vector3d direction, float t, float power)
    {
        released = false;
        m_body.Position = initial;
        m_body.Orientation = rot;
        m_body.LinearDamping = damping;
        m_body.AngularDamping = damping;

        m_body.AddForce(direction * power);
        isActive = true;
        StartCoroutine(Release(t));
    }

    IEnumerator Release(float t)
    {
        yield return new WaitForSeconds(t);
        if (!released)
        {
            m_body.ClearAccumulators();
            m_body.Velocity = Vector3d.Zero;
            isActive = false;
            Spawner.Instance.Release(_bulletMechanic);
        }
    }


    bool released = false;
    public void Collision(RigidBox rb)
    {
        if (gameObject.CompareTag("Bullet"))
        {
            //released = true;
            //m_body.ClearAccumulators();
            //m_body.Velocity = Vector3d.Zero;
            //isActive = false;
            //Spawner.Instance.Release(_bulletMechanic);
        }
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using Cyclone.Core;
//using Cyclone.Rigid;
//using Cyclone.Rigid.Constraints;
//using Cyclone.Rigid.Collisions;
//using Quaternion = Cyclone.Core.Quaternion;

//public class RigidSphere : MonoBehaviour
//{
//    public double mass = 1;

//    public double damping = 0.9;

//    private RigidBody m_body;

//    [SerializeField]
//    bool useGravity = true;

//    bool isActive = false;

//    [SerializeField]
//    BulletMechanic _bulletMechanic;

//    double scale;
//    Quaternion rot;
//    CollisionSphere shape;

//    private void Awake()
//    {
//        m_body = new RigidBody();
//    }

//    void Start()
//    {
//        scale = transform.localScale.y * 0.5;
//        rot = transform.rotation.ToQuaternion();

//        m_body.SetMass(mass);
//        m_body.SetAwake(true);
//        m_body.SetCanSleep(true);

//        shape = new CollisionSphere(scale);
//        shape.Body = m_body;

//        RigidPhysicsEngine.Instance.Bodies.Add(m_body);
//        RigidPhysicsEngine.Instance.Collisions.Primatives.Add(shape);
//    }

//    private void Update()
//    {
//        if (isActive)
//        {
//            transform.position = m_body.Position.ToVector3();
//            transform.rotation = m_body.Orientation.ToQuaternion();
//        }
//    }

//    public void SetInitial(Vector3d initial, Vector3d direction, float t, float power)
//    {
//        m_body.Position = initial;
//        m_body.Orientation = rot;
//        m_body.LinearDamping = damping;
//        m_body.AngularDamping = damping;

//        m_body.AddForce(direction * power);
//        isActive = true;
//        StartCoroutine(Release(t));
//    }

//    IEnumerator Release(float t)
//    {
//        yield return new WaitForSeconds(t);
//        m_body.ClearAccumulators();
//        m_body.Velocity = Vector3d.Zero;
//        isActive = false;
//        Spawner.Instance.Release(_bulletMechanic);
//    }


//}


