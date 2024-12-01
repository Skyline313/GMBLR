using System;
using _Source.Code.Components;
using _Source.Code.Events;
using _Source.Code.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Kuhpik;
using Supyrb;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Source.Code.Systems
{
    public class SecondGameSystem : GameSystemWithScreen<SecondGameScreen>
    {
        public override void OnInit()
        {
            Signals.Get<OnGamePointClick>().AddListener(HandleButtonClick);
        }

        public override void OnStateEnter()
        {
            game.CoinsPerRound = 0;
            game.CurrentRow = 0;
        }

        private async void HandleButtonClick(GamePointComponent point)
        {
            if(Bootstrap.Instance.GetCurrentGamestateID() != GameStateID.SecondGame) return;
            if (DOTween.IsTweening(screen.Player.transform)) return;

            var path = point.GetComponentInParent<PathComponent>();

            bool isMatch = path == game.SecondLevel.Paths[game.CurrentRow];
            
            if(!isMatch) return;

            bool isWin = Random.Range(0, 100) <= game.FirstGameWinChance;
            
            if (isWin)
            {
                point.Button.image.sprite = config.FirstGameSprites[1];

                game.CoinsPerRound = Mathf.RoundToInt(game.Bet * config.WinMultipliers[game.CurrentRow]);

                foreach (var pathPoints in path.Points)
                {
                    if(point == pathPoints) continue;
                    
                    if(Random.Range(0,100)>70)
                        pathPoints.Button.image.sprite = config.FirstGameSprites[1];
                    else
                        pathPoints.Button.image.sprite = config.FirstGameSprites[2];
                }
                
                game.CurrentRow++;
                point.Button.image.DOColor(Color.green, 0.1f).SetLoops(3, LoopType.Yoyo);

                if (game.CurrentRow >= game.SecondLevel.Paths.Length)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(2.25f));
                    Bootstrap.Instance.ChangeGameState(GameStateID.Result);
                }
                else
                {
                    screen.Player.transform.DOMoveX(point.transform.position.x, 1f).OnComplete(() =>
                    {
                        screen.Player.transform.DOMoveY(point.transform.position.y, 1f);
                    });
                }
            }
            else
            {
                point.Button.image.sprite = config.FirstGameSprites[2];
                point.Button.image.DOColor(Color.red, 0.1f).SetLoops(3, LoopType.Yoyo);
                
                foreach (var paths in game.SecondLevel.Paths)
                {
                    foreach (var rowNode in paths.Points)
                    {
                        if(point != rowNode)
                        {

                            bool chance = Random.Range(0, 100) > 70;
                            
                            if (chance)
                            {
                                rowNode.Button.image.color=Color.green;
                            }
                            else
                            {
                                rowNode.Button.image.sprite = config.FirstGameSprites[2];
                                rowNode.Button.image.color=Color.red;
                            }
                        }
                    }

                    var tempPoint = paths.Points[Random.Range(0, paths.Points.Length)];

                    while (tempPoint==point)
                    {
                        tempPoint = paths.Points[Random.Range(0, paths.Points.Length)];
                    }

                    tempPoint.Button.image.color=Color.green;
                }

                await UniTask.Delay(TimeSpan.FromSeconds(2.25f));

                player.Coins = Mathf.Clamp(player.Coins - game.Bet, 0, player.Coins);
                game.CoinsPerRound = 0;
                
                Bootstrap.Instance.ChangeGameState(GameStateID.Result);
            }
        }
    }
}