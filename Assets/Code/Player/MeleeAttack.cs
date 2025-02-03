using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{


    private Animator body;
    private Animator weapon;
    private void Start()
    {
        body = GetComponentInChildren<Animator>();
        weapon = transform.GetChild(1).GetComponentInChildren<Animator>();
        PlayerInputHandler.meleeAttackTriggered += Attack;
        PlayerInputHandler.meleeSpecialAttackTriggered += SpecialAttack;
    }

    private void Attack()
    {
        Debug.Log("Melee Attack Triggered");
        body.SetTrigger("Attack");
        weapon.SetTrigger("Attack");
    }
    private void SpecialAttack()
    {
        body.SetTrigger("SpecialAttack");
        weapon.SetTrigger("SpecialAttack");
    }
}
