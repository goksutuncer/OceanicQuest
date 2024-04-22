using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : StateBase
{
    [SerializeField] private DiverPlayer _player = null;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer = null;

    private MaterialPropertyBlock _materialPropertyBlock = null;

    public override void EnterActions()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();
        _skinnedMeshRenderer.GetPropertyBlock(_materialPropertyBlock);

        _player.CharacterController.enabled = false;
        StartCoroutine(MaterialDissolve());
    }

    public override void ExitActions()
    {
    }

    IEnumerator MaterialDissolve()
    {
        yield return new WaitForSeconds(2);
        float dissolveTimeDuration = 2f;
        float currentDissolveTime = 0;
        float dissolveHeight_start = 20f;
        float dissolveHeight_target = -10f;
        float dissolveHeight;

        _materialPropertyBlock.SetFloat("_enableDissolve", 1f);
        _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);

        while (currentDissolveTime < dissolveTimeDuration)
        {
            currentDissolveTime += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeight_start, dissolveHeight_target, currentDissolveTime / dissolveTimeDuration);
            _materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
            _skinnedMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
            yield return null;
        }
    }
}
