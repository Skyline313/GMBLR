using _Source.Code.UI;
using Kuhpik;
using UnityEngine;

namespace _Source.Code.Systems
{
    public class MenuSystem: GameSystemWithScreen<MenuScreen>
    {
        public Sprite FirstGameSprite;
        public Sprite SecondGameSprite;
        public Sprite FirstGameButtonSprite;
        public Sprite SecondGameButtonSprite;
        
        public override void OnInit()
        {
            game.Game = GameStateID.FirstGame;
            screen.SettingsButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(GameStateID.Settings));
            screen.StartGameButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(game.Game));
            screen.ChangeGameButton.onClick.AddListener(()=>ChangeGame());
            screen.InfoButton.onClick.AddListener(()=>screen.InfoPanel.gameObject.SetActive(true));
            screen.CloseInfoPanelButton.onClick.AddListener(()=>screen.InfoPanel.gameObject.SetActive(false));
        }

        public override void OnUpdate()
        {
            screen.CoinsText.SetText(player.Coins.ToString());
        }

        private void ChangeGame()
        {
            if(game.Game==GameStateID.FirstGame)
            {
                game.Game = GameStateID.SecondGame;
                screen.GameImage.sprite = SecondGameSprite;
                screen.ChangeGameButton.image.sprite = SecondGameButtonSprite;
                screen.ChangeGameButton.image.rectTransform.anchoredPosition = new Vector2(-415,-215);
                screen.GameInfoText.SetText("Howl Run");
            }
            else
            {
                game.Game = GameStateID.FirstGame;
                screen.GameImage.sprite = FirstGameSprite;
                screen.ChangeGameButton.image.sprite = FirstGameButtonSprite;
                screen.ChangeGameButton.image.rectTransform.anchoredPosition = new Vector2(415,-215);
                screen.GameInfoText.SetText("Moon Claw");
            }
        }
    }
}