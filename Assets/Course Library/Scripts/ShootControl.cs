using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Course_Library.Scripts
{
    public class ShootControl : MonoBehaviour
    {
        private PlayerInputs _playerControls;
        private LogicScript _logic;
        private InputAction _rotate;
        private InputAction _fire;
        private const float RotationSpeed = 300f;
        public GameObject projectile;
        private PlayerController _player;
        private float _moveRotation;
        
        [FormerlySerializedAs("shootSFX")] [SerializeField]
        private AudioSource shootSfx;
        
        private void Awake()
        {
            _playerControls = new PlayerInputs();
        }
        
        private void OnEnable()
        {
            _rotate = _playerControls.Player.Rotate;
            _rotate.Enable();
            _fire = _playerControls.Player.Fire;
            _fire.Enable();
            _fire.performed += Fire;
        }

        private void OnDisable()
        {
            _rotate.Disable();
            _fire.Disable();
        }

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        private void Update()
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
            if (!_player.GetIsAlive()) return;
            _moveRotation = _rotate.ReadValue<float>();
            transform.Rotate(Vector3.up * (RotationSpeed * Time.deltaTime * _moveRotation));
        }
    }
}
