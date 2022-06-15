using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using DG.Tweening;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    PlayerMechanics _mechanics;
    [SerializeField]
    Animator _animator;


    [SerializeField]
    CharacterController _controller;
    [SerializeField]
    Transform _camera;


    [SerializeField]
    float movementSpeed = 1;
    [SerializeField]
    float turnSmoothTime = 0.1f;
    [SerializeField]
    float turnSmoothVelocity;


    [SerializeField]
    Transform _playerLookAt;
    [SerializeField]
    Transform _player;

    float _gravityValue = -9.81f;
    float _jumpHeight = 3f;
    Vector3 _playerVelocity;
    Vector3 _direction;
    Vector3 _moveDirection;
    float _horizontal;
    float _vertical;
    float _targetAngle;
    float _angle;

    AnimationClip[] clips;
    float jumpingTime;
    float attakingTime;
    bool busy = false;

    private void Start()
    {
        _mechanics = GetComponent<PlayerMechanics>();

        clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Attack":
                    attakingTime = clip.length;
                    break;
                case "Jump":
                    jumpingTime = clip.length;
                    break;
            }
        }
    }

    bool inair = false;
    void Update()
    {
        if (!_controller.isGrounded && _playerVelocity.y > 0)
        {
            inair = true;
            //Up anim
        }

        if (!_controller.isGrounded && _playerVelocity.y < 0)
        {
            inair = true;
            //fall anim
        }
        if (_controller.isGrounded && inair)
        {
            inair = false;
            //land anim
        }


        if (_controller.isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            busy = true;
            _animator.SetBool("attack", true);
            StartCoroutine(AttackBusyTime(attakingTime));
            _mechanics.Attack(attakingTime);
            return;
        }

        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        _direction = new Vector3(_horizontal, 0, _vertical).normalized;


        if (_direction.magnitude >= 0.1f)
        {
            _animator.SetBool("walk", true);
            _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, _angle, 0f);

            _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
            _controller.Move(_moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("walk", false);
        }
        

        if (Input.GetKey(KeyCode.Space))
        {
            busy = true;
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _animator.SetBool("jump", true);
            StartCoroutine(JumpBusyTime(jumpingTime));
            return;
        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }


    IEnumerator JumpBusyTime(float t)
    {
        yield return new WaitForSeconds(t);
        _animator.SetBool("jump", false);
    }

    IEnumerator AttackBusyTime(float t)
    {
        yield return new WaitForSeconds(t);
        _animator.SetBool("attack", false);
    }






}
