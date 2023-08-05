using UnityEngine;

namespace Visuals
{
    using System;
    using Counters;
    using Enums;
    using Events;

    public class StoveCounterVisual : MonoBehaviour
    {
        [SerializeField] 
        private GameObject stoveOnGameObject;

        [SerializeField] 
        private GameObject particlesGameObject;

        [SerializeField] 
        private StoveCounter stoveCounter;

        private void Start()
        {
            stoveCounter.OnStateChanged += StoveCounterOnStateChanged;
        }

        private void StoveCounterOnStateChanged(object sender, OnStateChangedEventArgs e)
        {
            var showVisual = e.State is State.Frying or State.Fried;
            stoveOnGameObject.SetActive(showVisual);
            particlesGameObject.SetActive(showVisual);
        }
    }
}
