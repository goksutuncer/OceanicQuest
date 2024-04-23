using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : StateBase
{
    [SerializeField] private DiverPlayer _player = null;
    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderer = null;

    public override void EnterActions()
    {
        _player.CharacterController.enabled = false;
        StartCoroutine(MaterialDissolve());
    }

    public override void ExitActions()
    {
    }

    IEnumerator MaterialDissolve()
    {
        float dissolveTimeDuration = 2f;
        float currentDissolveTime = 0;
        float dissolveHeight_start = 20f;
        float dissolveHeight_target = -10f;
        float dissolveHeight;

        foreach (var item in _skinnedMeshRenderer)
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            item.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat("_enableDissolve", 1f);
            item.SetPropertyBlock(materialPropertyBlock);
        }

        while (currentDissolveTime < dissolveTimeDuration)
        {
            currentDissolveTime += Time.deltaTime;
            foreach (var item in _skinnedMeshRenderer)
            {
                dissolveHeight = Mathf.Lerp(dissolveHeight_start, dissolveHeight_target, currentDissolveTime / dissolveTimeDuration);
                MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
                materialPropertyBlock.SetFloat("_dissolve_height", dissolveHeight);
                item.SetPropertyBlock(materialPropertyBlock);
            }
            yield return null;
        }
        GameManager.Instance.GameOver();

    }
}
