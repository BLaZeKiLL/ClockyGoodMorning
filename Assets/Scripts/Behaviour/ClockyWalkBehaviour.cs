using ClokysGoodMorning.Controllers;

using UnityEngine;
using UnityEngine.Animations;

namespace ClokysGoodMorning.Behaviour {

    public class ClockyWalkBehaviour : StateMachineBehaviour {

        private bool stopped;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) {
            animator.GetComponent<ClockyAnimationController>().PlayWalkSound();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) {
            animator.GetComponent<ClockyAnimationController>().StopWalkSound();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
            AnimatorControllerPlayable controller) {
            if (Time.timeScale <= float.Epsilon) {
                animator.GetComponent<ClockyAnimationController>().StopWalkSound();
                stopped = true;
            } else if (stopped) {
                animator.GetComponent<ClockyAnimationController>().PlayWalkSound();
                stopped = false;
            }
        }

    }

}