using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeingHitState : StateBase
{
    [SerializeField] private DiverPlayer _player;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer = null;

    private MaterialPropertyBlock _materialPropertyBlock = null;
    
    public override void EnterActions()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
        _skinnedMeshRenderer.GetPropertyBlock(_materialPropertyBlock);

        //AddImpact();

        StartCoroutine(MaterialBlink());

    }

    public override void ExitActions()
    {
        
    }

    private void AddImpact(Vector3 attackerPos, float force)
    {
        Vector3 impactDirection = transform.position - attackerPos;
        impactDirection.Normalize();
        impactDirection.y = 0;
        //impactOnCharacter = impactDirection * force;
    }

    IEnumerator MaterialBlink()
    {
        _materialPropertyBlock.SetFloat("_blink", 0.4f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);

        yield return new WaitForSeconds(0.2f);

        _materialPropertyBlock.SetFloat("_blink", 0f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
        _player.PlayerStateController.ChangeState(EDiverPlayerState.Swim);
    }
}
