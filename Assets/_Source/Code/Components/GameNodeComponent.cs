using System;
using _Source.Code.Events;
using Supyrb;
using UnityEngine;
using UnityEngine.UI;

namespace _Source.Code.Components
{
    public class GameNodeComponent : MonoBehaviour
    {
        public Button Button;

        public void Start()
        {
            Button.onClick.AddListener((() =>
            {
                Signals.Get<OnGameNodeClick>().Dispatch(this);
            }));
        }
    }
}