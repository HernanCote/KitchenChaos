using System;
using ScriptableObjectDefinitions;
using UnityEngine;

namespace Counters
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject; 
            
        [SerializeField]
        private KitchenObjectSO kitchenObjectSo;
        
        public override void Interact(Player player)
        {
            if (player.HasKitchenObject()) 
                return;
            
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
