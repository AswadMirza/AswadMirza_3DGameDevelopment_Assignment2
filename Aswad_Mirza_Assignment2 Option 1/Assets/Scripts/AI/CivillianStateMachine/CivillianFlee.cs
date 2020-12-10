using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


//Aswad Mirza, 991445135 Exercise 4
//Based On Code from example 4 in week 9 
public class CivillianFlee : StateMachineBehaviour
{
    GameObject containingGameObject;

    CivillianAi civAiController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        containingGameObject = animator.gameObject;
        civAiController = animator.gameObject.GetComponent<CivillianAi>();
        civAiController.Flee(); 
    }

     

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (civAiController.Fled())
        {
            animator.SetBool("escaped", true);
        }
        else {
            //if we havnt fled this means that the player is still close to us
            animator.SetBool("escaped", false);
        }
    }
}
