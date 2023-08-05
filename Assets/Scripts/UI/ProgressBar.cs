using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   using System;
   using Counters;
   using Events;
   using Interfaces;

   public class ProgressBar : MonoBehaviour
   {
      [SerializeField] 
      private GameObject ProgressGameObject;
      [SerializeField] 
      private Image barImage;

      private IProgressable _progressable;
      
      private void Start()
      {
         _progressable = ProgressGameObject.GetComponent<IProgressable>();

         if (_progressable is null)
            throw new NullReferenceException("ProgressGameObject does not have a component that implements IProgressable");
         
         _progressable.OnProgressChanged += OnProgressChanged;
         barImage.fillAmount = 0;
         Hide();
      }

      private void OnProgressChanged(object sender, OnProgressChangedEventArgs e)
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
