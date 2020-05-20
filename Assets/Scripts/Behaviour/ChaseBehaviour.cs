using ClokysGoodMorning.Controllers;

using Pathfinding;

using UnityEngine;

namespace ClokysGoodMorning.Behaviour {

    public class ChaseBehaviour : StateMachineBehaviour {

        private HandyStateController _stateController;
        private HandyMovementController _movementController;
        private Seeker _seeker;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) {
            _seeker = animator.GetComponentInParent<Seeker>();
            _stateController = animator.GetComponentInParent<HandyStateController>();
            _movementController = animator.GetComponentInParent<HandyMovementController>();

            _stateController.PlayerInRange += PlayerInRange;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex) {
            _stateController.PlayerInRange -= PlayerInRange;
        }

        private void PlayerInRange(Vector3 selfPosition, Vector3 playerPosition) {
            _seeker.StartPath(selfPosition, playerPosition, path => {
                if (path.error) Debug.Log("Retry");
                else _movementController.Path = path;
            });
        }

    }

}