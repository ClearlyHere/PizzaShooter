using UnityEngine;

namespace Course_Library.Scripts
{
    public class DetectCollision : MonoBehaviour
    {
        private LogicScript _logic;

        private void Start()
        {
            _logic = GameObject.FindWithTag("Logic").GetComponent<LogicScript>();
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (gameObject.CompareTag("Animal") && otherCollider.CompareTag("Animal"))
            {
                // Pass, don't destroy
            }

            else if (gameObject.CompareTag("Player") || otherCollider.CompareTag("Player"))
            {
                // Pass, don't destroy
            }

            else if (gameObject.CompareTag("Food"))
            {
                _logic.AddScore();
                Destroy(gameObject);
            }

            else
            {
                Destroy(gameObject);
                Destroy(otherCollider.gameObject);
            }
        }
    }
}
