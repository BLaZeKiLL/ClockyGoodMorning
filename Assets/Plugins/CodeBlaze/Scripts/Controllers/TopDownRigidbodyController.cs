using System.ComponentModel;

using UnityEngine;

namespace CodeBlaze.Controllers {

    [RequireComponent(typeof(Rigidbody))]
    public class TopDownRigidbodyController : MonoBehaviour {

        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _groundDistance = 0.1f;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Camera _camera;
        
        [Description("Should the movment be according to global fixed axis or local relative axis")]
        [SerializeField] private bool _fixedAxisMovement;
        
        [Description("Distance from the player after which mouse rotation would be considered")]
        [Range(0.01f, 5f)]
        [SerializeField] private float _rotationSoftZone;

        public delegate void MovementUpdateHandler(Vector3 direction, float speed);
        public event MovementUpdateHandler MovementUpdate;

        public bool CanMove { get; set; } = true;
        
        private Rigidbody _rigidbody;
        private Vector3 _moveDirection;
        private bool _isGrounded;
        private Plane _ground;
        
        private void Start() {
            _rigidbody = GetComponent<Rigidbody>();
            _ground = new Plane(Vector3.up, Vector3.zero);
        }
        
        private void Update() {
            _isGrounded = CheckIsGrounded();
            GetInput();
        }

        private void FixedUpdate() {
            Move();
            Rotate();
        }
        
        private bool CheckIsGrounded() => Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        private void GetInput() {
            _moveDirection = 
                (_fixedAxisMovement ? Vector3.right : transform.right) * Input.GetAxis("Horizontal") +
                (_fixedAxisMovement ? Vector3.forward : transform.forward) * Input.GetAxis("Vertical");
            
            if (_moveDirection.magnitude >= 1f) _moveDirection = _moveDirection.normalized;
        }

        private void Move() {
            if (!CanMove) return;
            
            MovementUpdate?.Invoke(_moveDirection, _speed);
            
            var position = transform.position;
            _rigidbody.MovePosition(position + (_moveDirection * _speed * Time.fixedDeltaTime));
            _ground.SetNormalAndPosition(Vector3.up, new Vector3(0f, position.y, 0f));
        }
        
        private void Rotate() {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!_ground.Raycast(ray, out float length)) return;

            var look = ray.GetPoint(length);
            
            if (Vector3.Distance(look, transform.position) <= _rotationSoftZone) return;
            
            transform.LookAt(new Vector3(look.x, transform.position.y, look.z));
        }

    }

}