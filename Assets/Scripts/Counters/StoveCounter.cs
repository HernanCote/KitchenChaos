using UnityEngine;

namespace Counters
{
    using System;
    using System.Linq;
    using Enums;
    using Events;
    using Interfaces;
    using ScriptableObjectDefinitions;

    public class StoveCounter : BaseCounter, IProgressable
    {
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

        [SerializeField]
        private FryingRecipeSO[] fryingRecipes;

        private float _fryingTimer;

        private FryingRecipeSO _fryingRecipeSo;
        private State _currentState;

        private void Start()
        {
            _currentState = State.Idle;
        }

        private void Update()
        {
            if (!HasKitchenObject()) 
                return;

            switch (_currentState)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    CookKitchenObject(State.Fried);
                    break;
                case State.Fried:
                    CookKitchenObject(State.Burned);
                    break;
                case State.Burned:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CookKitchenObject(State nextState)
        {
            if (_fryingRecipeSo is null)
                return;
            
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                ProgressNormalized = _fryingTimer / _fryingRecipeSo.fryingTimerMax
            });

            _fryingTimer += Time.deltaTime;
            if (_fryingTimer < _fryingRecipeSo.fryingTimerMax)
                return;

            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(_fryingRecipeSo.output, this);
            _currentState = nextState;
            _fryingRecipeSo = GetFryingRecipeSo(GetKitchenObject().GetKitchenObjectSO());
            _fryingTimer = 0f;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                State = _currentState
            });

            if (_currentState == State.Burned)
            {
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    ProgressNormalized = 0f
                });
            }
                
        }

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                if (!player.HasKitchenObject())
                    return;

                if (!HasRecipeForInput(player.GetKitchenObject().GetKitchenObjectSO())) 
                    return;
                
                player.GetKitchenObject().SetKitchenObjectParent(this);
                _fryingRecipeSo = GetFryingRecipeSo(GetKitchenObject().GetKitchenObjectSO());

                _currentState = State.Frying;
                _fryingTimer = 0f;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    State = _currentState
                });
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    ProgressNormalized = _fryingTimer / _fryingRecipeSo.fryingTimerMax
                });
            }
            else
            {
                if (player.HasKitchenObject())
                    return;
            
                GetKitchenObject().SetKitchenObjectParent(player);
                _currentState = State.Idle;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    State = _currentState
                });
                
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    ProgressNormalized = 0f
                });
            }
        }
        
        private bool HasRecipeForInput(KitchenObjectSO input) => GetFryingRecipeSo(input) != null;
        private FryingRecipeSO GetFryingRecipeSo(KitchenObjectSO input) => fryingRecipes.FirstOrDefault(x => x.input == input);
        
    }
}
