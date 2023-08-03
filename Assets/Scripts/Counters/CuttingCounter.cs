namespace Counters
{
    using System;
    using System.Linq;
    using Events;
    using ScriptableObjectDefinitions;
    using UnityEngine;

    public class CuttingCounter : BaseCounter
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

        public event EventHandler OnCut;

        [SerializeField]
        private CuttingRecipeSO[] cuttingRecipes;

        private int _cuttingProgress;
        
        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (!player.HasKitchenObject())
                    return;
            
                player.GetKitchenObject().SetKitchenObjectParent(this);
                _cuttingProgress = 0;
                
                var cuttingRecipeSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                if (cuttingRecipeSo is null)
                    return;
                
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    ProgressNormalized = (float) _cuttingProgress / cuttingRecipeSo.cuttingProgressMax
                });
            }
            else
            {
                if (player.HasKitchenObject())
                    return;
            
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        
        public override void InteractAlternate(Player player)
        {
            if (!HasKitchenObject())
                return;

            var cuttingRecipeSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            if (cuttingRecipeSo == null)
                return;

            _cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                ProgressNormalized = (float) _cuttingProgress / cuttingRecipeSo.cuttingProgressMax
            });
            
            if (_cuttingProgress < cuttingRecipeSo.cuttingProgressMax) 
                return;
            
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cuttingRecipeSo.output, this);
        }
        
        private CuttingRecipeSO GetOutputForInput(KitchenObjectSO input)
        {
            var cuttingRecipe = cuttingRecipes.FirstOrDefault(x => x.input == input);

            return cuttingRecipe;
        }
    }
}
