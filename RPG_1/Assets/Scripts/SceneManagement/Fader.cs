using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();                    
        }
        public IEnumerator FadeOut(float time)
        {     
            while(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;

                //null sadece 1 frame beklememizi saglar.
                yield return null;
            }       
        }
        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;

                //null sadece 1 frame beklememizi saglar.
                yield return null;
            }
        }
    }
}
