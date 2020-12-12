using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aswad Mirza, 991445135 Exercise 4, edited for Assignment 2
//Based On Code from example 4 in week 9 
public class CivillianFindNextTarget : StateMachineBehaviour
{
	CivillianAi civillianAi;
	float timer = 2f;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("seeking next target");
		civillianAi = animator.gameObject.GetComponent<CivillianAi>();
		civillianAi.GetNextPatrolTarget();
		animator.SetBool("onTarget", false) ;
	}


	//because of a weird bug where the ai gets stuck in this state, whill tell it to go to the next patrol state.
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


		if (timer <= 0) {
			civillianAi.GetNextPatrolTarget();
			animator.SetBool("onTarget", false);
			timer = 2f;
		}

		timer -= Time.deltaTime;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		timer = 2f;
    }
}
