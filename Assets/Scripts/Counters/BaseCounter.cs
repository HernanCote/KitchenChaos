using UnityEngine;

namespace Counters
{
    using Interfaces;
    using ScriptableObjectDefinitions;

    public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        [SerializeField] 
        private Transform counterTopPoint;

        private KitchenObject _kitchenObject;
        
        public abstract void Interact(Player player);

        public virtual void InteractAlternate(Player player)
        {
            Debug.LogError("InteractAlternate from base counter called!");
        }
        
        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
        }
    
        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }
    
        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }
    
        public bool HasKitchenObject()
        {
            return _kitchenObject is not null;
        }
    }
}
