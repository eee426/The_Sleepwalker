using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTrap01 : BaseTrap
{
    public Animator animator;
    private void Start()
    {
        Arm();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }

   
    protected override void SetFixed()
    {
        animator.SetBool("Fixed", true);
        base.SetFixed();
    }

    public override void Arm()
    {
        base.Arm();
    }

    public override void OnCharacterEnterZone(SleepwalkerController character)
    {
        state= TrapState.Armed;
        base.OnCharacterEnterZone(character);
    }

    protected override void OnSafePass(SleepwalkerController character)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTriggerLethal(SleepwalkerController character)
    {
        character.Die();
    }
}
