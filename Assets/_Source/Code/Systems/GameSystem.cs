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
    public class GameSystem : GameSystemWithScreen<FirstGameScreen>
    {
        public override void OnInit()
        {
            Signals.Get<OnGameNodeClick>().AddListener(HandleButtonClick);
        }

        public override void OnStateEnter()
        {
            game.CoinsPerRound = 0;
            game.CurrentRow = 0;
        }

        private async void HandleButtonClick(GameNodeComponent node)
        {
            if(Bootstrap.Instance.GetCurrentGamestateID() != GameStateID.FirstGame) return;

            var row = node.GetComponentInParent<RowComponent>();

            bool isMatch = row == game.Level.Rows[game.CurrentRow];
            
            if(!isMatch) return;

            bool isWin = Random.Range(0, 100) <= game.FirstGameWinChance;
            
            if (isWin)
            {
                node.Button.image.sprite = config.FirstGameSprites[1];

                game.CoinsPerRound = Mathf.RoundToInt(game.Bet * config.WinMultipliers[game.CurrentRow]);

                foreach (var rowNode in row.Nodes)
                {
                    if(node == rowNode) continue;
                    
                    if(Random.Range(0,100)>70)
                        rowNode.Button.image.sprite = config.FirstGameSprites[1];
                    else
                        rowNode.Button.image.sprite = config.FirstGameSprites[2];
                }
                
                game.CurrentRow++;
                node.Button.image.DOColor(Color.green, 0.1f).SetLoops(2, LoopType.Yoyo);

                if (game.CurrentRow >= game.Level.Rows.Length)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(2.25f));
                    Bootstrap.Instance.ChangeGameState(GameStateID.Result);
                }
            }
            else
            {
                node.Button.image.sprite = config.FirstGameSprites[2];
                node.Button.image.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
                
                foreach (var levelRow in game.Level.Rows)
                {
                    foreach (var rowNode in levelRow.Nodes)
                    {
                        if(node != rowNode)
                        {

                            bool chance = Random.Range(0, 100) > 70;
                            
                            if (chance)
                            {
                                rowNode.Button.image.sprite = config.FirstGameSprites[1];
                            }
                            else
                            {
                                rowNode.Button.image.sprite = config.FirstGameSprites[2];
                            }
                        }
                    }

                    var tempNode = levelRow.Nodes[Random.Range(0, levelRow.Nodes.Length)];

                    while (tempNode==node)
                    {
                        tempNode = levelRow.Nodes[Random.Range(0, levelRow.Nodes.Length)];
                    }

                    tempNode.Button.image.sprite = config.FirstGameSprites[1];
                }

                await UniTask.Delay(TimeSpan.FromSeconds(2.25f));

                player.Coins = Mathf.Clamp(player.Coins - game.Bet, 0, player.Coins);
                game.CoinsPerRound = 0;
                
                Bootstrap.Instance.ChangeGameState(GameStateID.Result);
            }
        }
    }
}
