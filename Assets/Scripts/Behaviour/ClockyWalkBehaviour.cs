using ClokysGoodMorning.Controllers;

using UnityEngine;

namespace ClokysGoodMorning.Behaviour {

    public class ClockyWalkBehaviour : StateMachineBehaviour {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) {
            animator.GetComponent<ClockyAnimationController>().PlayWalkSound();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) {
            animator.GetComponent<ClockyAnimationController>().StopWalkSound();
        }

    }

}