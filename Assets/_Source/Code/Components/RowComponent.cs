using Sirenix.OdinInspector;
using UnityEngine;

namespace _Source.Code.Components
{
    public class RowComponent : MonoBehaviour
    {
        public GameNodeComponent[] Nodes;

        [Button]
        public void GetNodes()
        {
            Nodes = GetComponentsInChildren<GameNodeComponent>();
        }
    }
}
