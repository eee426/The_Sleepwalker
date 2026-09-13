using UnityEngine;
using UnityEngine.Events;

/// <summary>负责梦游者的死亡状态、死亡动画和死亡事件。</summary>
[DisallowMultipleComponent]
public class SleepwalkerDeath : MonoBehaviour
{
    [Header("死亡动画")]
    [SerializeField] private Animator animator;
    [Tooltip("Animator 中用于播放死亡动画的 Trigger 参数名")]
    [SerializeField] private string deathTriggerName = "Die";

    [Header("死亡事件")]
    [Tooltip("调用 Die() 时立即触发")]
    [SerializeField] private UnityEvent onDied;
    [Tooltip("由死亡动画最后一帧的 Animation Event 调用")]
    [SerializeField] private UnityEvent onDeathAnimationFinished;

    private SleepWalkerMove movement;
    private bool isDead;

    public bool IsDead => isDead;

    private void Awake()
    {
        movement = GetComponent<SleepWalkerMove>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (movement != null)
            movement.enabled = false;

        if (animator != null && !string.IsNullOrEmpty(deathTriggerName))
            animator.SetTrigger(deathTriggerName);

        onDied?.Invoke();
    }

    /// <summary>在死亡动画最后一帧添加同名 Animation Event。</summary>
    public void OnDeathAnimationFinished()
    {
        onDeathAnimationFinished?.Invoke();
    }
}
