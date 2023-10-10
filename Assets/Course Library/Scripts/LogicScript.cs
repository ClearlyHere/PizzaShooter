using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Course_Library.Scripts
{
    public class LogicScript : MonoBehaviour
    {
        public GameObject gameOverScreen;
        public GameObject scoreTextObject;
        public GameObject gameOverScore;
        private TMP_Text _gameOverText;
        private TMP_Text _scoreText;
        private int _score;
        private bool _gameOver;
        private PlayerController _player;

        [FormerlySerializedAs("hitSFX")] [SerializeField] private AudioSource hitSfx;
        [FormerlySerializedAs("loseSFX")] [SerializeField] private AudioSource loseSfx;
        [FormerlySerializedAs("bgmSFX")] [SerializeField] private AudioSource bgmSfx;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _scoreText = GameObject.Find("Text").GetComponent<TMP_Text>();
            _gameOverText = gameOverScore.GetComponent<TMP_Text>();
            gameOverScreen.SetActive(false);
        }

        private void Update()
        {
            GameOver();
        }

        public void AddScore()
        {
            if (_gameOver) return;
            _score++;
            hitSfx.Play();
            Debug.Log("Score: {_score}");
            UpdateScoreUI();
        }

        private void GameOver()
        {
            if (_player.GetIsAlive() || _gameOver) return;
            bgmSfx.Stop();
            loseSfx.Play();
            _gameOver = true;
            gameOverScreen.SetActive(true);
            UpdateGameOverScore();
        }

        private void UpdateGameOverScore()
        {
            if (_gameOver) _gameOverText.text = "Score:" + _score;
        }

        private void UpdateScoreUI()
        {
            if (_gameOver) return;
            scoreTextObject.SetActive(true);
            _scoreText.text = "Score:" + _score;
        }

        public bool IsGameOver()
        {
            return _gameOver;
        }

        public void RestartButton()
        {
            Debug.Log("Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}