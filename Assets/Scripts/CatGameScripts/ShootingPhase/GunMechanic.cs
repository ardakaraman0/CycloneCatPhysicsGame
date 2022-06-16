using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using Cyclone.Rigid.Collisions;

public class GunMechanic : MonoBehaviour
{
    [SerializeField]
    Transform _muzzlePos;
    [SerializeField]
    Transform _player;
    [SerializeField]
    PlayerMovement _playerM;
    float proximity = -1f;

    [SerializeField]
    GameObject _muzzleFlash;
    [SerializeField]
    GameObject _gun;
    [SerializeField]
    float _bulletLifetime = 1f;
    [SerializeField]
    float power = 10f;

    [SerializeField]
    AttackType _attackType = AttackType.bullet; 

    [SerializeField]
    float rate;
    float timer;

    [SerializeField]
    CinemachineFreeLook main_vcam;
    [SerializeField]
    CinemachineVirtualCamera vcam;
    [SerializeField]
    CinemachineBrain brain;
    Camera cam;
    [SerializeField]
    Image crosshair;
    [SerializeField]
    Transform _lookAt;

    BulletMechanic _bullet;

    Vector3d dir;

    bool activatable = false;
    bool isActive = false;
    bool canShoot = true;

    private void Start()
    {
        _playerM = _player.GetComponent<PlayerMovement>();
        cam = Camera.main;
    }

    void Activate()
    {
        _playerM.isFPS_state = true;
        main_vcam.gameObject.SetActive(false);
        vcam.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(true);
        isActive = true;
    }
    void Deactivate()
    {
        _playerM.isFPS_state = false;
        main_vcam.gameObject.SetActive(true);
        vcam.gameObject.SetActive(false);
        isActive = false;
        crosshair.gameObject.SetActive(false);
    }
    Ray ray;
    RaycastHit hit;
    private void Update()
    {
        if (!isActive)
        {
            if ((transform.position - _player.position).magnitude > proximity) return;
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Activate();
                }
                return;
            }
        }

        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            _lookAt.position = hit.point;
            dir = (_muzzlePos.position - _lookAt.position).normalized.ToVector3d();
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
        _bullet.SetAndFire(initial, dir, _bulletLifetime, power, _attackType);
        StartCoroutine(Muzzle());
    }

    IEnumerator Muzzle()
    {
        _muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(_bulletLifetime/2f);
        _muzzleFlash.SetActive(false);
    }
}
