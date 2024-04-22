using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwimState : StateBase
{
    [SerializeField] private DiverPlayer _player = null;
    [SerializeField] private float _moveSpeed = 5f;

    private float screenWidth;
    private float screenHeight;
    private Vector3 _movementVelocity;
    private bool _isInState;

    public override void EnterActions()
    {
        screenWidth = CalculateScreenWidth();
        screenHeight = CalculateScreenHeight();

        _isInState = true;
    }

    public override void ExitActions()
    {
        _isInState = false;
    }

    private void FixedUpdate()
    {
        if (_isInState)
        {
            CalculatePlayerMovement();

            _player.CharacterController.Move(_movementVelocity);

            if (Mathf.Abs(_player.transform.position.x) > screenWidth / 2)
            {
                // Wrap player around to the opposite side
                float wrapOffset = Mathf.Sign(_player.transform.position.x) * screenWidth;
                _player.transform.position -= new Vector3(wrapOffset, 0, 0);
            }
            Vector3 clampedPosition = _player.transform.position;
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenHeight / 2, screenHeight / 2);
            _player.transform.position = clampedPosition;
        }
    }

    private void CalculatePlayerMovement()
    {
        _movementVelocity.Set(_player.PlayerInput.HorizontalInput, _player.PlayerInput.VerticalInput, 0f);
        _movementVelocity.Normalize();
        _movementVelocity *= _moveSpeed * Time.deltaTime;
        if (_movementVelocity != Vector3.zero)
        {
            _player.transform.rotation = Quaternion.LookRotation(-1 * _movementVelocity);
        }
    }

    float CalculateScreenWidth()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return 0f;
        }

        float distanceFromCamera = Mathf.Abs(_player.transform.position.z - mainCamera.transform.position.z);
        return Mathf.Tan(mainCamera.fieldOfView * Mathf.Deg2Rad / 2) * distanceFromCamera * 2 * mainCamera.aspect;

    }

    float CalculateScreenHeight()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
            return 0f;
        }

        float distanceFromCamera = Mathf.Abs(_player.transform.position.z - mainCamera.transform.position.z);
        return Mathf.Tan(mainCamera.fieldOfView * Mathf.Deg2Rad / 2) * distanceFromCamera * 2;
    }

}
