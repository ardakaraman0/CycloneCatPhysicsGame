using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;

public class GunMechanic : MonoBehaviour
{
    [SerializeField]
    Transform _muzzlePos;


    [SerializeField]
    GameObject _muzzleFlash;
    [SerializeField]
    GameObject _gun;
    [SerializeField]
    float _bulletLifetime = 1f;
    [SerializeField]
    float power = 10f;

    [SerializeField]
    float rate;
    float timer;

    BulletMechanic _bullet;

    bool activatable = false;
    bool isActive = false;
    bool canShoot = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) activatable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) activatable = false;
    }

    void Activate()
    {
        isActive = true;
    }
    void Deactivate()
    {
        isActive = false;
    }

    private void Update()
    {
        if (!isActive)
        {
            if (!activatable) return;
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Activate();
                }
            }
        }

        timer += Time.deltaTime;
        if(timer >= rate)
        {
            canShoot = true;
        }

        if (Input.GetKey(KeyCode.Mouse0) && canShoot)
        {
            timer = 0;
            canShoot = false;
            Shoot(_muzzlePos.position.ToVector3d(), _muzzlePos.forward.ToVector3d(), _bulletLifetime);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Deactivate();
        }
    }

    public void Shoot(Vector3d initial, Vector3d direction, float t)
    {
        _bullet = Spawner.Instance.Get();
        _bullet.SetAndFire(initial, direction, _bulletLifetime, power);
        StartCoroutine(Muzzle());
    }

    IEnumerator Muzzle()
    {
        _muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(_bulletLifetime);
        _muzzleFlash.SetActive(false);
    }
}
