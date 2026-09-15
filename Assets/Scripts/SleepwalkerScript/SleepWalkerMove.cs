using UnityEngine;

/// <summary>只负责水平移动；状态机和死亡脚本通过公开方法控制启停。</summary>
public class SleepWalkerMove : MonoBehaviour
{
    public float speed = 5f;

    /// <summary>启动移动；由状态机切换到 Move 时调用。</summary>
    public void StartMoving()
    {
        enabled = true;
    }

    /// <summary>停止移动；Idle 和死亡都调用这一方法。</summary>
    public void StopMoving()
    {
        enabled = false;
    }

    private void Update()
    {
        // 停用组件后 Update 不再运行，速度值本身保持不变。
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
