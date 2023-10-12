using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Course_Library.Scripts
{
    public class ShootControl : MonoBehaviour
    {
        private PlayerInputs _playerControls;
        private LogicScript _logic;
        private InputAction _fire;
        public GameObject projectile;
        private PlayerController _player;
        private float _moveRotation;

        [SerializeField] private Camera mainCamera;

        [FormerlySerializedAs("shootSFX")] [SerializeField]
        private AudioSource shootSfx;

        private void Awake()
        {
            _playerControls = new PlayerInputs();
        }

        private void OnEnable()
        {
            _fire = _playerControls.Player.Fire;
            _fire.Enable();
            _fire.performed += Fire;
        }

        private void OnDisable()
        {
            _fire.Disable();
        }  

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        private void FixedUpdate()
        {
            ShooterRotation();
        }

        private void Fire(InputAction.CallbackContext context)
        {
            if (!_player.GetIsAlive()) return;
            shootSfx.Play();
            var transform1 = transform;
            Instantiate(projectile, transform1.position, transform1.rotation);
        }

        private void ShooterRotation()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit rayCast, float.MaxValue))
            {
                Vector3 shooterPosition = transform.position;
                Vector3 rayHit = rayCast.point;
                Vector3 direction = new Vector3(rayHit.x - shooterPosition.x, 0, rayHit.z - shooterPosition.z);
                transform.forward = direction;
            }
        }

    }
}