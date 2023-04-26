using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _turnSpeed = 1f;
    [SerializeField] private float _gravity = 9.8f;

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

        Vector3 _movementVector = transform.forward * _speed * Time.deltaTime * Input.GetAxis("Vertical");
        _movementVector.y = _gravity * Time.deltaTime * -1;

        _characterController.Move(_movementVector);
    }
}
