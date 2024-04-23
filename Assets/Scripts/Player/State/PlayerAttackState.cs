using System.Collections;
using UnityEngine;

public class PlayerAttackState : StateBase
{
    [SerializeField] private GameObject _weapon;

    [SerializeField] private DiverPlayer _player = null;

    [SerializeField] private Transform _weaponCarrier = null;

    GameObject _weaponInstance;

    public override void EnterActions()
    {
        StartCoroutine(AimRoutine());
    }

    public override void ExitActions()
    {
        Destroy(_weaponInstance);
    }

    IEnumerator AimRoutine()
    {
        CreateWeapon();

        while (true)
        {
            Aim();

            if (_player.PlayerInput.MouseButtonUp)
            {
                Fire();

                _player.PlayerStateController.ChangeState(EDiverPlayerState.Swim);

                yield break;
            }

            yield return null;
        }
    }

    void CreateWeapon()
    {
        _weaponInstance = Instantiate(
            _weapon,
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

    void Fire()
    {



    }
}
