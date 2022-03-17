using Features.GameUpdate;
using Features.GameUpdate.Features.GameUpdate;
using Features.Maps.Finishes;
using Features.Players;
using Features.Scores;
using Features.Views;

namespace Features.Games
{
    public class Game
    {
        private readonly Finish _finish;
        private readonly EndGameView _endGameView;
        private readonly Player _player;
        private readonly GameUpdates _gameUpdates;

        public Game(Player player, GameUpdates gameUpdates, Score score, Finish finish, EndGameView endGameView)
        {
            _player = player;
            _gameUpdates = gameUpdates;
            _finish = finish;
            _endGameView = endGameView;
        }

        public void Start()
        {
            _player.Died += OnDied;
            _finish.Reached += OnFinishReached;

            _gameUpdates.ResumeUpdate();
        }

        private void OnFinishReached()
        {
            EndGame();
        }

        private void OnDied()
        {
            _player.Died -= OnDied;
            EndGame();
        }
        
        private void EndGame()
        {
            _endGameView.Show();
            _gameUpdates.StopUpdate();
        }
    }
}