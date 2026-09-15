using UnityEngine;
using UnityEngine.Events;

/// <summary>处理死亡后的行为，并转发死亡动画播放完毕的通知。</summary>
[DisallowMultipleComponent]
public class SleepwalkerDeath : MonoBehaviour
{
    [Header("死亡事件")]
    [Tooltip("由死亡动画最后一帧的 Animation Event 调用")]
    [SerializeField] private UnityEvent onDeathAnimationFinished;

    private SleepWalkerMove movement;

    private void Awake()
    {
        // 死亡后的停止操作统一走移动脚本的封装方法。
        movement = GetComponent<SleepWalkerMove>();
    }

    /// <summary>Controller 切换到 Die 状态后调用；不使用 Animator Trigger。</summary>
    public void HandleDeath()
    {
        if (movement != null)
            movement.StopMoving();
    }

    /// <summary>在死亡动画最后一帧添加同名 Animation Event。</summary>
    public void OnDeathAnimationFinished()
    {
        onDeathAnimationFinished?.Invoke();
    }
}
