using DG.Tweening;
using Kuhpik;
using UnityEngine;

namespace _Source.Code.Systems
{
    public class LoadingScreenSystem : GameSystemWithScreen<LoadingScreen>
    {
        public GameObject loadingBar;

        public override void OnInit()
        {
            base.OnInit();

            loadingBar.transform.DORotate(Vector3.forward * 90, 1).SetLoops(-1, LoopType.Incremental);
        }
    }
}