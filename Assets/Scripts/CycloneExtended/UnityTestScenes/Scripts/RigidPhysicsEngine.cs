using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cyclone.Rigid;
using Cyclone.Rigid.Forces;


public class RigidPhysicsEngine : MonoBehaviour
{
    public int iterations = 0;

    public int maxContacts = 100;

    public double epsilon = 0.01;

    public double friction = 0.2f;

    public double restitution = 0;

    public static RigidBodyEngine Instance { get; private set; }

    private void Awake()
    {
        Instance = new RigidBodyEngine(maxContacts);
        Instance.Resolver.PositionIterations = iterations;
        Instance.Resolver.VelocityIterations = iterations;
        Instance.Resolver.PositionEpsilon = epsilon;
        Instance.Resolver.VelocityEpsilon = epsilon;
        Instance.Collisions.Restitution = restitution;
        Instance.Collisions.Friction = friction;

        Instance.ForceAreas.Add(new RigidGravityForce(-9.81));
    }

    private void FixedUpdate()
    {
        double dt = Time.fixedDeltaTime;

        Instance.StartFrame();
        Instance.RunPhysics(dt);
    }
}


