using UnityEngine;

namespace Himanshu
{
    [CreateAssetMenu(menuName = "ScrictableObjects/Collectable", fileName = "CollectableObject")]
    public class CollectableObject : ScriptableObject
    {
        public string m_objectName;
    }
}