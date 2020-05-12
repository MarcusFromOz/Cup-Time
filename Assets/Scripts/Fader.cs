using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        //** variables
        
        CanvasGroup canvasGroup;

        //** start and update
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            //*Testing the 3 different options
            //StartCoroutine(FadeOut(3f));
            //StartCoroutine(FadeIn(3f));
            //StartCoroutine(FadeOutIn());
        }

        //** public methods
        public IEnumerator FadeOutIn()
        {
            yield return FadeOut(2f);
            yield return FadeIn(2f);
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime/time;
                yield return null;  
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0 )
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

        //** private methods

    }
}

