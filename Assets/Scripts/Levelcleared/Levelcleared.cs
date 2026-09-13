using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelcleared : MonoBehaviour
{
    public Animator animator;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("isCleared",true);
    }
}
