using System;

using Pathfinding;

using UnityEngine;

namespace ClokysGoodMorning.Controllers {

    public class HandyMovementController : MonoBehaviour {

        [SerializeField] private float _nextWaypointDistance = 1f;
        [SerializeField] private float _speed = 2f;
        
        public Path Path {
            set {
                currentWaypoint = 0;
                ReachedEndOfPath = false;
                _path = value;
            }
        }

        public bool ReachedEndOfPath { get; private set; } = true;

        private Path _path;
        private int currentWaypoint;

        private void Update() {
            if (_path == null) return;
            
            // Check in a loop if we are close enough to the current waypoint to switch to the next one.
            // We do this in a loop because many waypoints might be close to each other and we may reach
            // several of them in the same frame.
            ReachedEndOfPath = false;
            float distanceToWaypoint;
            
            while (true) {
                // If you want maximum performance you can check the squared distance instead to get rid of a
                // square root calculation. But that is outside the scope of this tutorial.
                distanceToWaypoint = Vector3.Distance(transform.position, _path.vectorPath[currentWaypoint]);
                if (distanceToWaypoint < _nextWaypointDistance) {
                    // Check if there is another waypoint or if we have reached the end of the path
                    if (currentWaypoint + 1 < _path.vectorPath.Count) {
                        currentWaypoint++;
                    } else {
                        // Set a status variable to indicate that the agent has reached the end of the path.
                        // You can use this to trigger some special code if your game requires that.
                        ReachedEndOfPath = true;
                        break;
                    }
                } else {
                    break;
                }
            }

            // Slow down smoothly upon approaching the end of the path
            // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
            var speedFactor = ReachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / _nextWaypointDistance) : 1f;

            // Direction to the next waypoint
            // Normalize it so that it has a length of 1 world unit
            var dir = (_path.vectorPath[currentWaypoint] - transform.position).normalized;
            // Multiply the direction by our desired speed to get a velocity
            var velocity = dir * _speed * speedFactor;
            
            // Move the agent using the CharacterController component
            // Note that SimpleMove takes a velocity in meters/second, so we should not multiply by Time.deltaTime
            Move(velocity, _path.vectorPath[currentWaypoint]);
        }
        
        private void Move(Vector3 velocity, Vector3 direction) {
            velocity.y = 0;
            transform.position += velocity * Time.deltaTime;
            transform.LookAt(direction);
        }

    }

}