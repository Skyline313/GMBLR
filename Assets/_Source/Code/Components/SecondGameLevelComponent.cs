using Sirenix.OdinInspector;
using UnityEngine;

namespace _Source.Code.Components
{
    public class SecondGameLevelComponent : MonoBehaviour
    {
        public PathComponent[] Paths;
        
        [Button]
        public void GetPaths()
        {
            Paths = GetComponentsInChildren<PathComponent>();

            foreach (var path in Paths)
            {
                path.GetPaths();
            }
        }
    }
}