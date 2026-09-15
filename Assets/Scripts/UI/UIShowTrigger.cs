using UnityEngine;
using UnityEngine.Serialization;

/// <summary>启动时隐藏指定 UI；调用 ShowUI 时显示它并设置 Animator 的展开 Bool。</summary>
public class UIShowTrigger : MonoBehaviour
{
    [Header("目标 UI")]
    public GameObject targetUI;
    [FormerlySerializedAs("showTriggerName")]
    [Tooltip("目标 UI 的 Animator 中用于展开动画的 Bool 参数名")]
    public string showBoolName = "Expand";

    private void Awake()
    {
        if (targetUI == null)
        {
            Debug.LogWarning("UIShowTrigger 尚未指定目标 UI。", this);
            return;
        }

        // 脚本必须放在不会被隐藏的对象上，才能继续响应 Button.OnClick。
        if (transform.IsChildOf(targetUI.transform))
        {
            Debug.LogError("UIShowTrigger 请挂在目标 UI 之外的常驻对象上，不能挂在其子物体上。", this);
            return;
        }

        targetUI.SetActive(false);
    }

    /// <summary>可直接绑定到 Unity Button 的 OnClick。</summary>
    public void ShowUI()
    {
        if (targetUI == null)
        {
            Debug.LogWarning("UIShowTrigger 尚未指定目标 UI。", this);
            return;
        }

        targetUI.SetActive(true);
        SetExpanded(true);
    }

    /// <summary>返回按钮调用：复位展开参数，然后关闭目标 UI。</summary>
    public void ReturnUI()
    {
        if (targetUI == null)
        {
            Debug.LogWarning("UIShowTrigger 尚未指定目标 UI。", this);
            return;
        }

        SetExpanded(false);
        //targetUI.SetActive(false);
    }

    private void SetExpanded(bool expanded)
    {
        Animator animator = targetUI.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("目标 UI 上没有 Animator。", targetUI);
            return;
        }

        if (string.IsNullOrEmpty(showBoolName))
        {
            Debug.LogWarning("请填写展开动画的 Bool 参数名。", this);
            return;
        }

        // 名称和类型都要匹配，否则 Animator 不会进入预期过渡。
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name == showBoolName &&
                parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(showBoolName, expanded);
                return;
            }
        }

        Debug.LogWarning("目标 UI 的 Animator 中没有 Bool 参数：" + showBoolName, targetUI);
    }
}
