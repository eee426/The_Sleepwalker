using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapClickForwarder : MonoBehaviour
{
    private BaseTrap parentTrap;

    private void Awake()
    {
        // 自动找父物体（或更上层）身上的陷阱脚本
        parentTrap = GetComponentInParent<BaseTrap>();
    }
    
    private void OnMouseDown()
    {
        if (parentTrap != null)
        {
            parentTrap.OnPlayerInteract();
        }
    }
}
