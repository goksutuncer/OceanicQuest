using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeingHitState : StateBase
{
    [SerializeField] private DiverPlayer _player;
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderer = null;

    private MaterialPropertyBlock _materialPropertyBlock = null;
    
    public override void EnterActions()
    {
        if (_player.Health != null)
        {
            //TODO: IStateTransitionData
            _player.Health.ApplyDamage(30);
        }

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
        foreach (var item in _skinnedMeshRenderer)
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            item.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat("_blink", 0.4f);
            item.SetPropertyBlock(materialPropertyBlock);
        }

        yield return new WaitForSeconds(0.2f);

        foreach (var item in _skinnedMeshRenderer)
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            item.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat("_blink", 0f);
            item.SetPropertyBlock(materialPropertyBlock);
        }

        CheckHealth();


    }
    private void CheckHealth()
    {
        if (_player.Health.CurrentHealth <= 0)
        {
            _player.PlayerStateController.ChangeState(EDiverPlayerState.Dead);
        }
        else
        {
            _player.PlayerStateController.ChangeState(EDiverPlayerState.Swim);
        }
    }
}
