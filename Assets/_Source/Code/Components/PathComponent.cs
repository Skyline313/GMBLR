using Sirenix.OdinInspector;
using UnityEngine;

namespace _Source.Code.Components
{
    public class PathComponent : MonoBehaviour
    {
        public GamePointComponent[] Points;

        [Button]
        public void GetPaths()
        {
            Points = GetComponentsInChildren<GamePointComponent>();
        }
    }
}