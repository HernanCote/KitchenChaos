using UnityEngine;

namespace ScriptableObjectDefinitions
{
    [CreateAssetMenu]
    public class KitchenObjectSO : ScriptableObject
    {
        public Transform prefab;
        public Sprite sprite;
        public string objectName;
    }
}
