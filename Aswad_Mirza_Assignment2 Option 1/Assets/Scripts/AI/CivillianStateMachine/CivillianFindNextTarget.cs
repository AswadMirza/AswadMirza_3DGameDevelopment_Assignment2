using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aswad Mirza, 991445135 Exercise 4
//Based On Code from example 4 in week 9 
public class CivillianFindNextTarget : StateMachineBehaviour
{
	CivillianAi civillianAi;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("seeking next target");
		civillianAi = animator.gameObject.GetComponent<CivillianAi>();
		civillianAi.GetNextPatrolTarget();
	}
}
