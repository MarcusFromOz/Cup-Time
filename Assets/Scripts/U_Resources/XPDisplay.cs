using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class XPDisplay : MonoBehaviour
    {
        Experience experience;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}",experience.GetXP());
        }
    }
}

