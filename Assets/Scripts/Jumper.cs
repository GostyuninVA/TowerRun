using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private Rigidbody _rigidBody;
    private bool _isGrounded;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        _isGrounded = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
            _rigidBody.AddForce(Vector3.up * _jumpForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Road road))
        {
            _isGrounded = true;
        }
    }
}
