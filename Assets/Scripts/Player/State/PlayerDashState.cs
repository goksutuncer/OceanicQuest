using System.Collections;
using UnityEngine;

public class PlayerDashState : StateBase
{
    [SerializeField] private DiverPlayer _player = null;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashSpeed = 5f;

    public override void EnterActions()
    {
        StartCoroutine(Dash());
    }

    public override void ExitActions()
    {
    }

    IEnumerator Dash()
    {
        float timePassed = 0;

        while(timePassed < _dashDuration)
        {
            timePassed += Time.deltaTime;

            _player.CharacterController.Move(_player.transform.forward * _dashSpeed * -1);

            yield return null;
        }

        _player.PlayerStateController.ChangeState(EDiverPlayerState.Swim);
    }
}
