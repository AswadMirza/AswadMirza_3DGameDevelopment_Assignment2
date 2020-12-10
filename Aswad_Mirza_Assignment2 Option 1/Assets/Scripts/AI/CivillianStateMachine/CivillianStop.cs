using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Aswad Mirza, 991445135 Exercise 4
//Based On Code from example 4 in week 9 
public class CivillianStop : StateMachineBehaviour
{
	GameObject containingGameObject;

	CivillianAi civAiController;
	public float timer;
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		containingGameObject = animator.gameObject;
		civAiController = animator.gameObject.GetComponent<CivillianAi>();
	
		timer = civAiController.reloadTime;
		civAiController.ClearNav();

	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			civAiController.Reload();
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool("escaped", false);
	}
}
