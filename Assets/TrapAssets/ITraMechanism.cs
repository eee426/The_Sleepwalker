using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有陷阱机关必须实现的核心接口
/// </summary>
public interface ITrapMechanism
{
    /// <summary>陷阱当前状态</summary>
    TrapState State { get; }

    /// <summary>
    /// 玩家点击/交互该陷阱时调用
    /// 具体是"点一下修复"还是"长按修复"由子类自行处理
    /// 这里只是统一入口
    /// </summary>
    void OnPlayerInteract();

    /// <summary>
    /// 梦游者进入该陷阱触发区域时调用（由角色移动逻辑或碰撞体回调）
    /// </summary>
    void OnCharacterEnterZone(SleepwalkerController character);

    /// <summary>
    /// 重置陷阱状态（关卡重玩/复用对象池时用）
    /// </summary>
    void ResetTrap();
}

/// <summary>
/// 陷阱状态机
/// </summary>
public enum TrapState
{
    Idle,       // 尚未激活/尚未进入触发范围
    Armed,      // 危险中，等待玩家处理
    Fixed,      // 已被玩家修复，安全
    Triggered   // 已触发，梦游者已经历陷阱（无论安全通过还是惊醒）
}
