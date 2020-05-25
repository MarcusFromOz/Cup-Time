using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;
    
        void Update()
        {
            float healthPercentage = healthComponent.GetFraction();

            if (Mathf.Approximately(healthPercentage, 1) || Mathf.Approximately(healthPercentage, 0))
            {
                rootCanvas.enabled = false;
                return;
            }
            else
            {
                foreground.localScale = new Vector3(healthPercentage, 1f, 1f); 
                rootCanvas.enabled = true;
            }
        }
    }
}