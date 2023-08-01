using Interfaces;
using ScriptableObjectDefinitions;
using UnityEngine;

namespace Counters
{
    public class ClearCounter : BaseCounter
    {
        [SerializeField]
        private KitchenObjectSO kitchenObjectSo;
        
        public override void Interact(Player player)
        {
           
        }
    }
}
