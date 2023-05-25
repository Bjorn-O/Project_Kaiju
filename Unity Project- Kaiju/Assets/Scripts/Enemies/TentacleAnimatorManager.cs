using System;
using System.Collections;
using System.Collections.Generic;
using Toolbox.Attributes;
using UnityEngine;
using UnityEngine.Serialization;

public class TentacleAnimatorManager : MonoBehaviour
{
    private static readonly int Death = Animator.StringToHash("Death");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Throw = Animator.StringToHash("Throw");

    [SerializeField] private Animator animator;
    [SerializeField] private Material glowMaterial;
    
    private bool _isStaggeringAnimation;
    private SkinnedMeshRenderer _renderer;
    private static readonly int Scale = Shader.PropertyToID("GlowScale");

    private void Start()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        if (!_isStaggeringAnimation) return;
        var materialProperty = new MaterialPropertyBlock();
        
        materialProperty.SetFloat(Scale, 2);
        _renderer.SetPropertyBlock(materialProperty);
    }

    public IEnumerator HoldAnimationForSeconds(float timeToStop, float timeUntilStop = 0)
    {
        yield return new WaitForSeconds(timeUntilStop);
        animator.speed = 0;
        _isStaggeringAnimation = true;
        yield return new WaitForSeconds(timeToStop);
        animator.speed = 1;
        _isStaggeringAnimation = false;
    }

    public void DeathAnimTrigger() {animator.SetTrigger(Death);}
    public void AttackAnimTrigger() {animator.SetTrigger(Attack);}
    public void ThrowAnimTrigger() {animator.SetTrigger(Throw);}
}
