using UnityEngine;

/// <summary>每个关卡放置一个；请挂在始终启用的对象上，而不是暂停面板上。</summary>
[DefaultExecutionOrder(-100)]
[DisallowMultipleComponent]
public class PauseManager : MonoBehaviour
{
    [Header("暂停界面（可暂时不挂）")]
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Animator pauseAnimator;
    [Tooltip("Animator 中控制暂停入场动画的 Bool 参数名。留空则只显示面板。")]
    [SerializeField] private string pauseParameter = "IsPaused";

    [Header("场景设置")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private static PauseManager owner;
    private bool paused;
    private float previousTimeScale = 1f;
    private bool hasPauseParameter;

    public static bool IsPaused => owner != null && owner.paused;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        owner = null;
    }

    private void Awake()
    {
        // 隐藏面板不能把管理器自身也关闭，否则无法再响应 Esc。
        if (pauseUI != null && transform.IsChildOf(pauseUI.transform))
        {
            Debug.LogError("PauseManager 必须挂在暂停面板外的常驻对象上。", this);
            pauseUI = null;
            pauseAnimator = null;
        }

        if (pauseAnimator != null)
        {
            // Time.timeScale 为 0 时，暂停 UI 的动画仍继续播放。
            pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
            foreach (AnimatorControllerParameter parameter in pauseAnimator.parameters)
            {
                if (parameter.name == pauseParameter && parameter.type == AnimatorControllerParameterType.Bool)
                {
                    hasPauseParameter = true;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(pauseParameter) && !hasPauseParameter)
                Debug.LogWarning("暂停 Animator 缺少 Bool 参数：" + pauseParameter, this);
        }

        SetUI(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    // 以下公开方法可直接绑定到 Unity Button 的 OnClick。
    public void TogglePause()
    {
        if (paused) Resume();
        else Pause();
    }

    public void Pause()
    {
        if (!isActiveAndEnabled || paused || IsPaused ||
            gameObject.scene.name == mainMenuSceneName)
            return;

        owner = this;
        previousTimeScale = Time.timeScale;
        paused = true;
        Time.timeScale = 0f;
        SetUI(true);
    }

    public void Resume()
    {
        if (!paused) return;

        paused = false;
        Time.timeScale = previousTimeScale;
        if (owner == this) owner = null;
        SetUI(false);
    }

    private void SetUI(bool visible)
    {
        if (visible && pauseUI != null) pauseUI.SetActive(true);
        if (pauseAnimator != null && hasPauseParameter)
            pauseAnimator.SetBool(pauseParameter, visible);
        if (!visible && pauseUI != null) pauseUI.SetActive(false);
    }

    private void OnDisable()
    {
        // 切换场景或关闭管理器时，不能把暂停状态遗留给下一场景。
        Resume();
    }
}
