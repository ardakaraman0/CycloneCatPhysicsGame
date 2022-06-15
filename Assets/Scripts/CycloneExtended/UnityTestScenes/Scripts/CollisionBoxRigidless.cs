using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone.Core;

using BOX = Cyclone.Rigid.Collisions.CollisionBox;

namespace CycloneUnityTestScenes
{
    public class CollisionBoxRigidless : MonoBehaviour
    {
        private BOX m_box;

        void Start()
        {
            var scale = transform.localScale.ToVector3d() * 0.5;

            m_box = new BOX(scale);

            RigidPhysicsEngine.Instance.Collisions.Primatives.Add(m_box);
        }
    }
}
