using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed;

    private InputAction _shipMoveInput;
    private Vector2 _inputDir;
    private Vector3 _moveDir;

    private void Awake()
    {
        playerInput.enabled = false;
        _shipMoveInput = playerInput.actions["Move"];
    }

    public void InitializeMovement()
    {
        playerInput.enabled = true;
    }

    private void Update()
    {
        _inputDir = _shipMoveInput.ReadValue<Vector2>();
        _moveDir = new Vector3(_inputDir.x, 0, _inputDir.y);
        transform.position += _moveDir * speed * Time.deltaTime;
    }
}
