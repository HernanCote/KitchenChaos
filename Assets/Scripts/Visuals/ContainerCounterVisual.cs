using UnityEngine;
using Counters;

namespace Visuals
{
    using System;

    public class ContainerCounterVisual : MonoBehaviour
    {
        private const string OPEN_CLOSE = "OpenClose";
        
        [SerializeField] 
        private ContainerCounter containerCounter;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            containerCounter.OnPlayerGrabbedObject += ContainerCounterOnPlayerGrabbedObject; 
        }

        private void ContainerCounterOnPlayerGrabbedObject(object sender, EventArgs e)
        {
            _animator.SetTrigger(OPEN_CLOSE);
        }
    }
}
