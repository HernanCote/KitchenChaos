using UnityEngine;

namespace Counters
{
    public class TrashCounter : BaseCounter
    {
        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject())
                return;
            
            player.GetKitchenObject().DestroySelf();
        }
    }
}
