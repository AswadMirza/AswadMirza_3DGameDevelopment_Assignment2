using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aswad Mirza, 991445135 Exercise 4
//Based On Code from example 4 in week 9 
public class CivillianChase : StateMachineBehaviour
{
    GameObject containingObject;

    CivillianAi civAiController;

    int navTimer = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        containingObject = animator.gameObject;
        civAiController = animator.gameObject.GetComponent<CivillianAi>();
        navTimer = 0; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (navTimer > 10)
        {
            navTimer = 0;
            civAiController.Chase();
        }
        navTimer++;

        if (civAiController.InAttackRange()) {
            animator.SetBool("inAttackRange", true);
        }
           

        if (!civAiController.IsPlayerVisible())
        {
            Debug.Log("lost track of player");
            civAiController.GetNextPatrolTarget();
            animator.SetBool("isPlayerVisible", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        civAiController.ClearNav();

    }
}
