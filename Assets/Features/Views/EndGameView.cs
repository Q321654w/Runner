using Features.Scores;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Features.Views
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private Text _scoreView;
        [SerializeField] private Button _restartButton;
        private Score _score;

        public void Initialize(Score score)
        {
            _restartButton.onClick.AddListener(Restart);
            _score = score;
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _scoreView.text = "Your Score: " + _score.Value();
        }
    }
}