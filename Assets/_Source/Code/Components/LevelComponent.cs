using Sirenix.OdinInspector;
using UnityEngine;

namespace _Source.Code.Components
{
    public class LevelComponent : MonoBehaviour
    {
        public RowComponent[] Rows;
        
        [Button]
        public void GetRows()
        {
            Rows = GetComponentsInChildren<RowComponent>();

            foreach (var row in Rows)
            {
                row.GetNodes();
            }
        }
    }
}