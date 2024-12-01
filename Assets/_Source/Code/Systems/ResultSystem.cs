using _Source.Code.UI;
using Kuhpik;

namespace _Source.Code.Systems
{
    public class ResultSystem : GameSystemWithScreen<ResultScreen>
    {
        public override void OnInit()
        {
            screen.NextGameButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(GameStateID.Menu));
        }

        public override void OnStateEnter()
        {
            screen.CoinsText.SetText(game.CoinsPerRound.ToString());
            player.Coins += game.CoinsPerRound;
        }
    }
}