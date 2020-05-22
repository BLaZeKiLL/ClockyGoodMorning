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

            var position = animator.transform.position;
            
            _seeker.StartPath(position, PickRandomPoint(position), path => {
                if (path.error) Debug.Log("Retry");
                else _controller.Path = path;
            });
        }

        private Vector3 PickRandomPoint (Vector3 position) {
            var point = Random.insideUnitSphere * _wanderRadius;
            point.y = 0;
            point += position;
            return point;
        }

    }

}