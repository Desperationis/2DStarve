using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorAttackTest : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("OnAttack");
        }
    }
}
