using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 movement;
    [SerializeField]
    private float turnSpeed;
    private Animator _animator;
    private Quaternion rotation = Quaternion.identity;
    private Rigidbody _rigidbody;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal,0,vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        _animator.SetBool("isWalking",isWalking);
        if (isWalking)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward,
            movement, turnSpeed * Time.fixedDeltaTime,0f);
        rotation = Quaternion.LookRotation(desiredForward);
        

    }

    private void OnAnimatorMove()
    {
        
        //S = S0 + delta S
        // ESTE TRUCO SE HACE CUANDO LA ANIMACION YA APORTA MOVIMIENTO
        _rigidbody.MovePosition(_rigidbody.position+ movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
        _rigidbody.MoveRotation(rotation);

    }
}
