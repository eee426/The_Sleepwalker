using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>终点碰撞区域：有物体进入时播放通关界面动画。</summary>
public class Levelcleared : MonoBehaviour
{
    public Animator animator;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 当前实现不筛选碰撞者；终点区域应在场景中避免其他物体进入。
        animator.SetBool("isCleared",true);
    }
}
