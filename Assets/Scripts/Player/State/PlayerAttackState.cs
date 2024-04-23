using System.Collections;
using UnityEngine;

public class PlayerAttackState : StateBase
{
    [SerializeField] private GameObject _weaponPrefab;
    [SerializeField] private DiverPlayer _player = null;

    [SerializeField] private Transform _weaponCarrier = null;
    GameObject _weaponInstance;
    private bool isFired;
    private bool isInState;

    public override void EnterActions()
    {
        isInState = true;
        isFired = false;
        StartCoroutine(AimRoutine());
    }

    public override void ExitActions()
    {
        isInState = false;
        if(!isFired)
        {
            Destroy(_weaponInstance);
        }
    }

    IEnumerator AimRoutine()
    {
        CreateWeapon();

        while (isInState)
        {
            Aim();

            if (_player.PlayerInput.MouseButtonUp)
            {
                _weaponInstance.GetComponent<Weapon>().Fire();
                isFired = true;

                _player.PlayerStateController.ChangeState(EDiverPlayerState.Swim);

                yield break;
            }

            yield return null;
        }
    }

    void CreateWeapon()
    {
        _weaponInstance = Instantiate(
            _weaponPrefab,
            _weaponCarrier);
    }

    void Aim()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;

        Vector3 viewportPoint = Camera.main.ScreenToViewportPoint(mousePosition);
        float target = (viewportPoint.y - 0.5f) * 2;

        Vector3 currentRotation = _weaponCarrier.localRotation.eulerAngles;

        currentRotation.x = target * -90;

        _weaponCarrier.localRotation = Quaternion.Euler(currentRotation);
    }

}
