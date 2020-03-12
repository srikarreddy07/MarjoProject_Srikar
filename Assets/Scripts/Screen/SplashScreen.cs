using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class SplashScreen : MonoBehaviour
    {
        [Header("Fade")]
        [SerializeField] float waitToFade = 2f;
        [SerializeField] ScreenFader screenFader;

        private void Awake()
        {
            screenFader = GetComponent<ScreenFader>();
        }
            
        private void Start()
        {
            screenFader.FadeOn();
        }

        public void FadeAndLoad ()
        {
            StartCoroutine(FadeAndLoadRoutine());
        }

        IEnumerator FadeAndLoadRoutine ()
        {
            LevelLoader.LoadMainMenuLevel();

            yield return new WaitForSeconds(waitToFade);

            screenFader.Fadeoff();

            yield return new WaitForSeconds(screenFader.FadeDuration);

            Object.Destroy(gameObject);
        }
    } 
}
