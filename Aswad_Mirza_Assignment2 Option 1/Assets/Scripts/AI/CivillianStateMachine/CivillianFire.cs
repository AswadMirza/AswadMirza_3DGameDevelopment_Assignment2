using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aswad Mirza, 991445135 Exercise 4
//Based On Code from example 4 in week 9 
public class CivillianFire : StateMachineBehaviour
{
    
    GameObject containingGameObject;

    CivillianAi civAiController;

    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        containingGameObject = animator.gameObject;
        civAiController = animator.gameObject.GetComponent<CivillianAi>();
        timer = civAiController.GetFireTimer();
        
        animator.SetBool("reloaded", false);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            civAiController.Fire();
            animator.SetInteger("ammo", animator.GetInteger("ammo") - 1);
            timer = civAiController.GetFireTimer();
        }
        if (civAiController.InAttackRange())
        {
            containingGameObject.transform.LookAt(civAiController.player.transform);
        }
        else
        {
            animator.SetBool("inAttackRange", false);
        }
    }
}
