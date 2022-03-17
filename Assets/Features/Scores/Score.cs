using Features.Players;

namespace Features.Scores
{
    public class Score
    {
        private readonly Player _player;

        public Score(Player player)
        {
            _player = player;
        }

        public int Value()
        {
            return _player.Cubes.Count;
        }
    }
}