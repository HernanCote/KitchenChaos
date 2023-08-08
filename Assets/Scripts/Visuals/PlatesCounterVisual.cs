namespace Visuals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Counters;
    using UnityEngine;

    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] 
        private Transform counterTopPoint;
        [SerializeField] 
        private Transform plateVisualPrefab;
        [SerializeField]
        private PlatesCounter platesCounter;

        
        private IList<GameObject> _plateVisuals;

        private void Awake()
        {
            _plateVisuals = new List<GameObject>();
        }

        private void Start()
        {
            platesCounter.OnPlateSpawned += PlatesCounterOnPlateSpawned;
            platesCounter.OnPlateRemoved += PlatesCounterOnPlateRemoved;
        }

        private void PlatesCounterOnPlateRemoved(object sender, EventArgs e)
        {
            if (_plateVisuals.Count <= 0)
                return;
            
            var plate = _plateVisuals.Last();
            _plateVisuals.Remove(plate);
            Destroy(plate);
        }

        private void PlatesCounterOnPlateSpawned(object sender, EventArgs e)
        {
            var plateVisual = Instantiate(plateVisualPrefab, counterTopPoint);
            var plateOffsetY = .1f;
            plateVisual.localPosition = new Vector3(0f, plateOffsetY * _plateVisuals.Count, 0f);
            _plateVisuals.Add(plateVisual.gameObject);
        }
    }
}