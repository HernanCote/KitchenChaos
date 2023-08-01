using Counters;
using Events;
using UnityEngine;

namespace Visuals
{
    public class SelectedCounterVisual : MonoBehaviour
    {

        [SerializeField] 
        private BaseCounter baseCounter;
        [SerializeField] 
        private GameObject[] visualGameObjects;
    
        private void Start()
        {
            Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
        }

        private void PlayerOnSelectedCounterChanged(object sender, OnSelectedCounterEventArgs e)
        {
            if (e.SelectedCounter == baseCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            foreach (var visualGameObject in visualGameObjects)
            {
                visualGameObject.SetActive(true);    
            }
        }

        private void Hide()
        {
            foreach (var visualGameObject in visualGameObjects)
            {
                visualGameObject.SetActive(false);    
            }
        }
    }
}
