using System;
using _Source.Code.Components;
using _Source.Code.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

namespace _Source.Code.Systems
{
    public class GameUISystem : GameSystemWithScreen<FirstGameScreen>
    {
        
        public override void OnInit()
        {
            game.Levels = screen.transform.GetComponentsInChildren<LevelComponent>(true);
            
            screen.ExitButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(GameStateID.Menu));
            screen.TakeWinButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(GameStateID.Result));
            screen.SettingsButton.onClick.AddListener(()=>Bootstrap.Instance.ChangeGameState(GameStateID.Settings));
            
            screen.EasyLevelButton.onClick.AddListener(() =>
            {
                screen.EasyLevelButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
                screen.MediumLevelButton.transform.localScale = Vector3.one;
                screen.HardLevelButton.transform.localScale = Vector3.one;

                game.FirstGameWinChance = config.FirstGameChance[0];
                
            });
            
            screen.MediumLevelButton.onClick.AddListener(() =>
            {
                screen.MediumLevelButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
                screen.EasyLevelButton.transform.localScale = Vector3.one;
                screen.HardLevelButton.transform.localScale = Vector3.one;

                game.FirstGameWinChance = config.FirstGameChance[1];
                
            });
            
            screen.HardLevelButton.onClick.AddListener(() =>
            {
                screen.HardLevelButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
                screen.MediumLevelButton.transform.localScale = Vector3.one;
                screen.EasyLevelButton.transform.localScale = Vector3.one;

                game.FirstGameWinChance = config.FirstGameChance[2];
                
            });
            
            screen.FourRowsButton.onClick.AddListener(() =>
            {
                game.Level = game.Levels[0];
                game.Level.gameObject.SetActive(true);
                
                game.Levels[1].gameObject.SetActive(false);
                game.Levels[2].gameObject.SetActive(false);
                
                screen.FourRowsButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
                screen.FiveRowsButton.transform.localScale = Vector3.one;
                screen.SixRowsButton.transform.localScale = Vector3.one;
                
                game.CurrentRow = 0;
                PrepareRows();

            });
            
            screen.FiveRowsButton.onClick.AddListener(() =>
            {
                game.Level = game.Levels[1];
                game.Level.gameObject.SetActive(true);
                
                game.Levels[0].gameObject.SetActive(false);
                game.Levels[2].gameObject.SetActive(false);
                
                screen.FiveRowsButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
                screen.FourRowsButton.transform.localScale = Vector3.one;
                screen.SixRowsButton.transform.localScale = Vector3.one;
                
                game.CurrentRow = 0;
                PrepareRows();

            });
            
            screen.SixRowsButton.onClick.AddListener(() =>
            {
                game.Level = game.Levels[2];
                game.Level.gameObject.SetActive(true);
                
                game.Levels[0].gameObject.SetActive(false);
                game.Levels[1].gameObject.SetActive(false);
                
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
            
            screen.EasyLevelButton.transform.DOScale(Vector3.one * 1.1f, 0.1f);
            screen.MediumLevelButton.transform.localScale = Vector3.one;
            screen.HardLevelButton.transform.localScale = Vector3.one;

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
            foreach (var row in game.Level.Rows)
            {
                foreach (var node in row.Nodes)
                {
                    node.Button.image.sprite = config.FirstGameSprites[0];
                }
            }
        }
    }
}