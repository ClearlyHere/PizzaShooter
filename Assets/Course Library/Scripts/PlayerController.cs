using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Course_Library.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        // Player input
        private PlayerInputs _playerControls;
        public GameObject projectile;

        private int _lives = 3;
        private bool _isAlive = true;

        private LogicScript _logic;
        private InputAction _move;
        private InputAction _fire;

        private const float MoveSpeed = 15f;
        private const float MovementRestriction = 20f;
        private const float TopBound = 31f;
        private const float BottomBound = 10f;
        private Vector3 _moveDirection = Vector3.zero;

        [FormerlySerializedAs("shootSFX")] [SerializeField]
        private AudioSource shootSfx;

        [FormerlySerializedAs("hurtSFX")] [SerializeField]
        private AudioSource hurtSfx;

        [FormerlySerializedAs("dieSFX")] [SerializeField]
        private AudioSource dieSfx;

        private void Awake()
        {
            _playerControls = new PlayerInputs();
        }

        private void Start()
        {
            _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        }

        private void OnEnable()
        {
            _move = _playerControls.Player.Move;
            _move.Enable();
            _fire = _playerControls.Player.Fire;
            _fire.Enable();
            _fire.performed += Fire;
        }

        private void OnDisable()
        {
            _move.Disable();
            _fire.Disable();
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (!otherCollider.CompareTag("Animal") || _lives < 1 || _logic.IsGameOver()) return;
            switch (_lives)
            {
                case > 1:
                    hurtSfx.Play();
                    break;
                case 1:
                    dieSfx.Play();
                    break;
            }
            LoseLife();
        }

        // Update is called once per frame
        private void Update()
        {
            PlayerMovement();
            InvisibleWalls();
            Die();
        }

        // Taking player's input and using it for movement
        private void PlayerMovement()
        {
            if (!_isAlive) return;
            _moveDirection = _move.ReadValue<Vector3>();
            transform.Translate(Vector3.right * (MoveSpeed * Time.deltaTime * _moveDirection.x));
            transform.Translate(Vector3.forward * (MoveSpeed * Time.deltaTime * _moveDirection.z));
        }

        // Limiting player's movement in camera
        private void InvisibleWalls()
        {
            var transform1 = transform;
            var position = transform1.position;
            // Right wall
            if (position.x > MovementRestriction)
            {
                position = new Vector3(MovementRestriction, position.y, position.z);
                transform1.position = position;
            }

            // Left wall
            if (position.x < -MovementRestriction)
            {
                position = new Vector3(-MovementRestriction, position.y, position.z);
                transform1.position = position;
            }

            if (position.z > TopBound)
            {
                position = new Vector3(position.x, position.y, TopBound);
                transform1.position = position;
            }

            if (position.z < BottomBound)
            {
                position = new Vector3(position.x, position.y, BottomBound);
                transform1.position = position;
            }
        }

        private void Fire(InputAction.CallbackContext context)
        {
            if (!_isAlive) return;
            shootSfx.Play();
            var transform1 = transform;
            Instantiate(projectile, transform1.position, transform1.rotation);
        }

        private void Die()
        {
            if (_lives <= 0 && _isAlive) _isAlive = false;
        }

        private void LoseLife()
        {
            _lives--;
        }

        public int GetLives()
        {
            return _lives;
        }

        public bool GetIsAlive()
        {
            return _isAlive;
        }
    }
}