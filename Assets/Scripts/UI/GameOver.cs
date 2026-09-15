using UnityEngine;

/// <summary>控制失败界面的展开动画；由梦游者死亡动画结束事件调用。</summary>
public class GameOver : MonoBehaviour
{
    [SerializeField] private Animator animator;

    /// <summary>
    /// 可绑定到死亡动画结束事件或其他 UnityEvent。
    /// </summary>
    public void ShowGameOver()
    {
        if (animator == null)
        {
            // 引用缺失时只提示，避免失败流程抛异常。
            Debug.LogWarning("GameOver 尚未挂载 Animator。", this);
            return;
        }

        animator.SetBool("isExpand", true);
    }
}
