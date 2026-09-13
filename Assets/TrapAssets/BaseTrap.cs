using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 陷阱基类：处理通用的点击检测、状态流转、与角色的交互判定
/// 具体陷阱（尖刺、绳桥、摆斧等）继承此类，只需重写关键钩子方法
/// </summary>
//[RequireComponent(typeof(Collider2D))]
//使用这个基础类型的时候，一定要记得子物体的脚本挂载的物体底下的子物体，一定要挂载着转发的脚本以及碰撞箱才能正常收到碰撞，要不然就没用
public abstract class BaseTrap : MonoBehaviour, ITrapMechanism
{
    [Header("通用配置")]
    
    /*[SerializeField] protected float interactRadius = 0.5f;*/ // 点击容错半径，可选


    protected TrapState state = TrapState.Idle;

    public TrapState State => state;
    

    protected virtual void OnMouseDown()
    {
        // 通用点击检测，具体"点击算不算有效"可以在这里加冷却/范围判断
        if (state == TrapState.Armed)
        {
            OnPlayerInteract();
        }
    }

    public virtual void OnPlayerInteract()
    {
        if (PauseManager.IsPaused) return;

        // 默认行为：点一下直接修复
        // 需要"长按/连点/拖拽"的陷阱，子类重写这个方法即可
        SetFixed();
    }

    public virtual void OnCharacterEnterZone(SleepwalkerController character)
    {
        if (state == TrapState.Fixed)
        {
            OnSafePass(character);
        }
        else if (state == TrapState.Armed)
        {
            OnTriggerLethal(character);
        }

        state = TrapState.Triggered;
    }

    /// <summary>陷阱被成功修复时调用（子类可重写做动画/音效）</summary>
    protected virtual void SetFixed()
    {
        state = TrapState.Fixed;
    }

    /// <summary>梦游者安全通过该陷阱时的表现，子类实现</summary>
    protected abstract void OnSafePass(SleepwalkerController character);

    /// <summary>梦游者踩中未修复陷阱时的表现，子类实现（比如惊醒判定）</summary>
    protected abstract void OnTriggerLethal(SleepwalkerController character);

    public virtual void ResetTrap()
    {
        state = TrapState.Idle;
    }

    /// <summary>供关卡加载时统一"激活"陷阱用</summary>
    public virtual void Arm()
    {
        state = TrapState.Armed;
    }
}
