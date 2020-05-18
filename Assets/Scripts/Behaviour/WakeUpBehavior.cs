using ClokysGoodMorning.Entities;

using UnityEngine;

namespace ClokysGoodMorning.Behaviour {

    public class WakeUpBehavior : StateMachineBehaviour {
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.GetComponentInParent<Hoomans>().HoomansAwake();
        }

    }

}