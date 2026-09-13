using System;
using UnityEngine;

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
        death = GetComponent<SleepwalkerDeath>();
        movement = GetComponent<SleepWalkerMove>();

        if (animator == null)
            animator = GetComponent<Animator>();

        hasStateParameter = HasIntParameter(stateParameterName);
    }

    private void Start()
    {
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
        if (IsDead)
            return;

        ApplyState(SleepwalkerState.Die);
        death.Die();
    }

    private void ApplyState(SleepwalkerState newState, bool force = false)
    {
        if (!force && currentState == newState)
            return;

        currentState = newState;
        movement.enabled = currentState == SleepwalkerState.Move;

        if (animator != null && hasStateParameter)
            animator.SetInteger(stateParameterName, (int)currentState);

        StateChanged?.Invoke(currentState);
    }

    private bool HasIntParameter(string parameterName)
    {
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
