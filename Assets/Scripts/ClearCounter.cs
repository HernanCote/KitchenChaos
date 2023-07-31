using ScriptableObjectDefinitions;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField]
    private KitchenObject kitchenObject;
    [SerializeField] 
    private Transform counterTopPoint;
    
    public void Interact()
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObject.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
    }
}
