using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : StateBase
{
    public override void EnterActions()
    {
        StartCoroutine(DashSpeed());
    }

    public override void ExitActions()
    {
    }

    IEnumerator DashSpeed()
    {
        //_moveSpeed = 15f;
        //yield return new WaitForSeconds(0.5f);
        //_moveSpeed = 3.5f;
        //SwitchStateTo(CharacterState.Normal);
        yield break;
    }
}
