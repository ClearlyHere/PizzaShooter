using UnityEngine;
using UnityEngine.UI;

namespace Course_Library.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        private PlayerController _player;
        private Image _fill;
        // Start is called before the first frame update
        private void Start()
        {
            _player = GameObject.Find("Player").GetComponent<PlayerController>();
            _fill = GameObject.Find("Fill").GetComponent<Image>();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            if (_player.GetLives() == 2)
            {
                _fill.fillAmount = 0.666f;
            }

            if (_player.GetLives() == 1)
            {
                _fill.fillAmount = 0.333f;
            }

            if (_player.GetLives() == 0)
            {
                _fill.fillAmount = 0f;
            }
        }
    }
}
