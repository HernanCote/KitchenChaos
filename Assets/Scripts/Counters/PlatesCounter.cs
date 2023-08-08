using UnityEngine;

namespace Counters
{
    using System;
    using ScriptableObjectDefinitions;

    public class PlatesCounter : BaseCounter
    {
        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;
        
        private const float SPAWN_TIMER_MAX = 4f;
        private const int SPAWN_PLATES_AMOUNT_MAX = 4;
        
        [SerializeField]
        private KitchenObjectSO plateKitchenObjectSo;
        
        private float spawnTimer;
        private int platesSpawnedAmount; 


        private void Update()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= SPAWN_TIMER_MAX)
            {
                spawnTimer = 0f;
                if (platesSpawnedAmount < SPAWN_PLATES_AMOUNT_MAX)
                {
                    ++platesSpawnedAmount;
                    OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void Interact(Player player)
        {
            if (player.HasKitchenObject() || platesSpawnedAmount <= 0) 
                return;
            
            platesSpawnedAmount--;
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSo, player);
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
