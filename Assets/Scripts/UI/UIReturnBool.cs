using UnityEngine;

/// <summary>返回按钮入口：将调用转交给不会随面板隐藏的显示脚本。</summary>
public class UIReturnBool : MonoBehaviour
{
    [Header("显示脚本")]
    [SerializeField] private UIShowTrigger uiShow;

    /// <summary>可直接绑定到返回按钮的 OnClick。</summary>
    public void ReturnUI()
    {
        if (uiShow == null)
        {
            Debug.LogWarning("UIReturnBool 尚未指定 UIShowTrigger。", this);
            return;
        }

        uiShow.ReturnUI();
    }
}
