using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   using System;
   using Counters;
   using Events;

   public class ProgressBar : MonoBehaviour
   {
      [SerializeField] 
      private CuttingCounter cuttingCounter;
      [SerializeField] 
      private Image barImage;

      private void Start()
      {
         cuttingCounter.OnProgressChanged += CuttingCounterOnProgressChanged;
         barImage.fillAmount = 0;
         Hide();
      }

      private void CuttingCounterOnProgressChanged(object sender, OnProgressChangedEventArgs e)
      {
         barImage.fillAmount = e.ProgressNormalized;
         
         barImage.color = e.ProgressNormalized switch
         {
            < 0.25f => Color.red,
            < 0.75f => Color.yellow,
            >= 0.75f => Color.green,
         };
         
         if (e.ProgressNormalized is 0f or 1f)
            Hide();
         else
            Show();
      }

      private void Show()
      {
         gameObject.SetActive(true);
      }

      private void Hide()
      {
         gameObject.SetActive(false);
      }
   }
}