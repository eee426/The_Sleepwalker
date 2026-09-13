using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTrap02 : BaseTrap
{
    
    [SerializeField] private float minSwipeDistance = 1.0f; // 最小滑动距离，太短不算有效滑动
    public Animator animator;
    private Vector2 startPos;
    private bool isDragging = false;

    private void Start()
    {
        Arm();
    }

    protected override void OnMouseDown()
    {
        if (PauseManager.IsPaused) return;
        
        // 鼠标刚点下去的瞬间，记录起始位置
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;

        
    }

    private void Update()
    {
        if (PauseManager.IsPaused) isDragging = false;
    }

    private void OnMouseUp()
    {
        if (PauseManager.IsPaused) { isDragging = false; return; }
        if (!isDragging) return;
        isDragging = false;

        Vector2 endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 delta = endPos - startPos; // 结束位置减起始位置，得到滑动的方向和距离

        if (delta.magnitude < minSwipeDistance)
        {
            // 滑动距离太短，不算数（可能只是手抖点了一下）
            return;
        }

        // 判断主要是往左还是往右（这里先只判断左右，忽略上下）
        if (delta.x > 0)
        {
            OnSwipeRight();
        }
        else
        {
            OnSwipeLeft();
        }
    }

    protected virtual void OnSwipeRight()
    {
        Debug.Log("向右滑动");
        OnPlayerInteract();
    }

    protected virtual void OnSwipeLeft()
    {
        Debug.Log("向左滑动");
    }

    protected override void SetFixed()
    {
        animator.SetBool("Fixed", true);
        base.SetFixed();
    }

    public override void Arm()
    {
        base.Arm();
    }
    public override void OnPlayerInteract()
    {
        base.OnPlayerInteract();
    }

    protected override void OnSafePass(SleepwalkerController character)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTriggerLethal(SleepwalkerController character)
    {
        throw new System.NotImplementedException();
    }
}