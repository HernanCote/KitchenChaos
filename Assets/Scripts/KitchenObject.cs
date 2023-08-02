using Interfaces;
using ScriptableObjectDefinitions;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] 
    private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent _kitchenObjectParent;
    
    public KitchenObjectSO GetKitchenObjectSO() => kitchenObjectSO;
    public IKitchenObjectParent GetKitchenObjectParent() => _kitchenObjectParent;

    public static KitchenObject SpawnKitchenObject(
        KitchenObjectSO kitchenObjectSo,
        IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        
        var kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        
        return kitchenObject;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (_kitchenObjectParent is not null)
        {
            _kitchenObjectParent.ClearKitchenObject();
        }
        
        _kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Kitchen Parent already has a kitchen object!");
        }
        
        kitchenObjectParent.SetKitchenObject(this);
        
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public void DestroySelf()
    {
        _kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
