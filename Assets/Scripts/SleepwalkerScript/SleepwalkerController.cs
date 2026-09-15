using System;
using UnityEngine;

/// <summary>梦游者状态机：切状态、同步 Animator，并通知移动/死亡组件。</summary>
[RequireComponent(typeof(SleepWalkerMove))]
[RequireComponent(typeof(SleepwalkerDeath))]
public class SleepwalkerController : MonoBehaviour
{
    [Header("状态机")]
    [SerializeField] private SleepwalkerState initialState = SleepwalkerState.Move;

    [Header("动画联动")]
    [SerializeField] private Animator animator;
    [Tooltip("Animator 中用于接收状态枚举值的 Int 参数名")]
    [SerializeField] private string stateParameterName = "State";

    private SleepwalkerDeath death;
    private SleepWalkerMove movement;
    private SleepwalkerState currentState;
    private bool hasStateParameter;

    public SleepwalkerState CurrentState => currentState;
    public bool IsDead => currentState == SleepwalkerState.Die;

    public event Action<SleepwalkerState> StateChanged;

    private void Awake()
    {
        // RequireComponent 保证两个行为组件存在；Animator 可手动指定。
        death = GetComponent<SleepwalkerDeath>();
        movement = GetComponent<SleepWalkerMove>();

        if (animator == null)
            animator = GetComponent<Animator>();

        hasStateParameter = HasIntParameter(stateParameterName);
    }

    private void Start()
    {
        // 初始 Die 要走完整死亡流程；其他状态仅同步状态与移动。
        if (initialState == SleepwalkerState.Die)
            Die();
        else
            ApplyState(initialState, true);
    }

    /// <summary>切换到 Idle 或 Move；传入 Die 时会完整执行死亡逻辑。</summary>
    public void SetState(SleepwalkerState newState)
    {
        if (newState == SleepwalkerState.Die)
        {
            Die();
            return;
        }

        // 死亡是最终状态，死亡后不能重新进入 Idle 或 Move。
        if (IsDead)
            return;

        ApplyState(newState);
    }

    public void SetIdle()
    {
        SetState(SleepwalkerState.Idle);
    }

    public void StartMoving()
    {
        SetState(SleepwalkerState.Move);
    }

    /// <summary>供陷阱等外部逻辑调用。</summary>
    public void Die()
    {
        // 死亡不可重复执行，避免重复触发后续逻辑。
        if (IsDead)
            return;

        ApplyState(SleepwalkerState.Die);
        death.HandleDeath();
    }

    private void ApplyState(SleepwalkerState newState, bool force = false)
    {
        // force 用于 Start：枚举默认值可能恰好等于配置的初始状态。
        if (!force && currentState == newState)
            return;

        currentState = newState;
        // 状态机只决定何时启动/停止；具体移动操作由 Move 脚本负责。
        if (currentState == SleepwalkerState.Move)
            movement.StartMoving();
        else if (currentState == SleepwalkerState.Idle)
            movement.StopMoving();

        // State 是 Int 参数（Idle=0、Move=1、Die=2），不是 Animator Trigger。
        if (animator != null && hasStateParameter)
            animator.SetInteger(stateParameterName, (int)currentState);

        StateChanged?.Invoke(currentState);
    }

    private bool HasIntParameter(string parameterName)
    {
        // 参数缺失时跳过写入，避免 Animator 报错。
        if (animator == null || string.IsNullOrEmpty(parameterName))
            return false;

        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == parameterName &&
                parameter.type == AnimatorControllerParameterType.Int)
                return true;
        }

        return false;
    }
}

public enum SleepwalkerState
{
    Idle = 0,
    Move = 1,
    Die = 2
}
