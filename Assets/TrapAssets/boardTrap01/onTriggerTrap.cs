using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTriggerTrap : MonoBehaviour
{
    //这个脚本挂在子物体上，用作判定
    private BaseTrap parentTrap;

    private void Awake()
    {
        // 自动找父物体（或更上层）身上的陷阱脚本
        parentTrap = GetComponentInParent<BaseTrap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Sleepwalker"))
        {
            
            parentTrap.OnCharacterEnterZone(collision.gameObject.GetComponent<SleepwalkerController>());
        }
    }
}
