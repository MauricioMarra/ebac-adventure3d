using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _turnSpeed = 1f;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private float _vSpeed = 0f;

    [SerializeField] private Animator _animator;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * _turnSpeed * Time.deltaTime, 0);

        var verticallInput = Input.GetAxis("Vertical");
        Vector3 _movementVector = transform.forward * _speed * verticallInput;

        _animator.SetBool("Run", verticallInput != 0);

        Jump();
        _movementVector.y = _vSpeed;

        _characterController.Move(_movementVector * Time.deltaTime);
    }

    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _vSpeed = _jumpForce;
        }

        _vSpeed -= _gravity * Time.deltaTime;
        _vSpeed = Mathf.Clamp(_vSpeed, _gravity * -1, _jumpForce * 2);
    }
}
