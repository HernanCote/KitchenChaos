using UnityEngine;

namespace Visuals
{
    using System;
    using Counters;
    using UnityEngine.Serialization;

    public class CuttingCounterVisual : MonoBehaviour
    {
        private const string CUT = "Cut";
        
        [SerializeField] 
        private CuttingCounter cuttingCounter;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            cuttingCounter.OnCut += CuttingCounterOnCut; 
        }

        private void CuttingCounterOnCut(object sender, EventArgs e)
        {
            _animator.SetTrigger(CUT);
        }
    }
}
