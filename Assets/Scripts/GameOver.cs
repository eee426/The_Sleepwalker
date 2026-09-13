using UnityEngine;

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
            Debug.LogWarning("GameOver 尚未挂载 Animator。", this);
            return;
        }

        animator.SetBool("isExpand", true);
    }
}
