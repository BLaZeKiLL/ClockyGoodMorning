using ClokysGoodMorning.Controllers;

using Pathfinding;

using UnityEngine;

namespace ClokysGoodMorning.Behaviour {

    public class WanderBehavior : StateMachineBehaviour {

        [SerializeField] private float _wanderRadius = 4f;

        private HandyMovementController _controller;
        private Seeker _seeker;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            _seeker = animator.GetComponentInParent<Seeker>();
            _controller = animator.GetComponentInParent<HandyMovementController>();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!_controller.ReachedEndOfPath) return;

            var transform = animator.transform;
            
            _seeker.StartPath(transform.position, PickRandomPoint(transform), path => {
                if (path.error) Debug.Log("Retry");
                else _controller.Path = path;
            });
        }

        private Vector3 PickRandomPoint (Transform transform) {
            var point = Random.insideUnitSphere * _wanderRadius;
            point.y = 0;
            point += transform.position;
            return point;
        }

    }

}