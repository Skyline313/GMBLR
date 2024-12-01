using _Source.Code.Components;
using _Source.Code.UI;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

namespace _Source.Code.Systems
{
    public class SecondGameUISystem : GameSystemWithScreen<SecondGameScreen>
    {
        public override void OnInit()
        {
            game.SecondLevels = screen.transform.GetComponentsInChildren<SecondGameLevelComponent>(true);
            
            screen.ExitButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(GameStateID.Menu));
            screen.TakeWinButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(GameStateID.Result));
            screen.SettingsButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(GameStateID.Settings));
            
            screen.FourRowsButton.onClick.AddListener(() =>
            {
                game.SecondLevel = game.SecondLevels[0];
                game.SecondLevel.gameObject.SetActive(true);
                
                game.SecondLevels[1].gameObject.SetActive(false);
                game.SecondLevels[2].gameObject.SetActive(false);
                
                screen.FourRowsButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
                screen.FiveRowsButton.transform.localScale = Vector3.one;
                screen.SixRowsButton.transform.localScale = Vector3.one;
                
                game.CurrentRow = 0;
                PrepareRows();

            });
            
            screen.FiveRowsButton.onClick.AddListener(() =>
            {
                game.SecondLevel = game.SecondLevels[1];
                game.SecondLevel.gameObject.SetActive(true);
                
                game.SecondLevels[0].gameObject.SetActive(false);
                game.SecondLevels[2].gameObject.SetActive(false);
                
                screen.FiveRowsButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
                screen.FourRowsButton.transform.localScale = Vector3.one;
                screen.SixRowsButton.transform.localScale = Vector3.one;
                
                game.CurrentRow = 0;
                PrepareRows();

            });
            
            screen.SixRowsButton.onClick.AddListener(() =>
            {
                game.SecondLevel = game.SecondLevels[2];
                game.SecondLevel.gameObject.SetActive(true);
                
                game.SecondLevels[0].gameObject.SetActive(false);
                game.SecondLevels[1].gameObject.SetActive(false);
                
                screen.SixRowsButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
                screen.FourRowsButton.transform.localScale = Vector3.one;
                screen.FiveRowsButton.transform.localScale = Vector3.one;

                game.CurrentRow = 0;
                
                PrepareRows();
            });
            
            screen.MinBetButton.onClick.AddListener(() =>
            {
                game.Bet = Mathf.Clamp(game.Bet - 50, 1, player.Coins);
            });
            
            screen.MaxBetButton.onClick.AddListener(() =>
            {
                game.Bet = Mathf.Clamp(game.Bet + 50, 1, player.Coins);
            });
        }

        public override void OnStateEnter()
        {
            game.Level = game.Levels[0];
            game.Level.gameObject.SetActive(true);
                
            game.Levels[1].gameObject.SetActive(false);
            game.Levels[2].gameObject.SetActive(false);
                
            screen.FourRowsButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
            screen.FiveRowsButton.transform.localScale = Vector3.one;
            screen.SixRowsButton.transform.localScale = Vector3.one;
            
            PrepareRows();
            
            game.FirstGameWinChance = config.FirstGameChance[0];

            game.CoinsPerRound = 0;
            
            game.Bet = Mathf.Clamp(50, 1, player.Coins);
        }

        public override void OnUpdate()
        {
            screen.CoinsText.SetText(player.Coins.ToString());
            screen.CurrentText.SetText(game.Bet.ToString());
            screen.CurrentWinText.SetText(game.CoinsPerRound.ToString());
        }

        private void PrepareRows()
        {
            foreach (var path in game.SecondLevel.Paths)
            {
                foreach (var point in path.Points)
                {
                    point.Button.image.sprite = config.SecondGameSprites[0];
                }
            }
        }
    }
}