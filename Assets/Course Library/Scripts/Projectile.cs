using UnityEngine;

namespace Course_Library.Scripts
{
    public class Projectile : MonoBehaviour
    {
        private const float FoodMoveSpeed = 40f;
        private const float AnimalMoveSpeed = 12.5f;
        private const float SideBound = 27f;
        private const float TopBound = 40f;
        private const float BottomBound = 3f;

        // Update is called once per frame
        private void Update()
        {
            Movement();
            DestroyOffBounds();
        }

        private void Movement()
        {
            if (gameObject.CompareTag($"Food"))
                transform.Translate(Vector3.forward * (FoodMoveSpeed * Time.deltaTime));

            else if (gameObject.CompareTag($"Animal"))
                transform.Translate(Vector3.forward * (AnimalMoveSpeed * Time.deltaTime));
        }

        private void DestroyOffBounds()
        {
            if (transform.position.x > SideBound || transform.position.x < -SideBound ||
                transform.position.z < BottomBound) Destroy(gameObject);

            if (transform.position.z > TopBound) Destroy(gameObject);
        }
    }
}