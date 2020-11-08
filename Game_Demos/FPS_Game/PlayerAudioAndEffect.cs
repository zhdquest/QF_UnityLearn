using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioAndEffect : StateMachineBehaviour
{
	public bool fire = true;
	public bool reload = true;
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		FPSGun currentGun = animator.GetComponent<FPSGun>();
		
		if (fire)
		{
			//获取开火声音
			AudioClip fireClip = currentGun.fireClip;
			//播放
			AudioSource.PlayClipAtPoint(fireClip,animator.transform.position);
			//播放特效
			currentGun.PlayerFireEffect();
		}

		if (reload)
		{
			//获取换弹声音
			AudioClip reloadClip = currentGun.reloadClip;
			//播放
			AudioSource.PlayClipAtPoint(reloadClip,animator.transform.position);
		}
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
