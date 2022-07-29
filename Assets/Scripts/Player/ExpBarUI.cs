using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ExpBarUI : MonoBehaviour
    {
        [SerializeField] private Slider expSlider;
        [SerializeField] float desiredDuration = 3f;
        [SerializeField] private AnimationCurve curve;

        private Coroutine ongoingGradualChange;
        private void Awake()
        {
            LoadSavedValue();
        }

        

        public void AnimateChanges(float endPoint)
        {
            
            float startPoint = expSlider.value;
            //expSlider.value = endPoint;
            if(ongoingGradualChange != null)
            {
                StopCoroutine(ongoingGradualChange);
            }
            ongoingGradualChange = StartCoroutine(GradualChange(startPoint, endPoint));
            
        }
        
        IEnumerator GradualChange(float startPoint, float endPoint)
        {
            float elapsedTime = 0;
            while (elapsedTime < desiredDuration)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / desiredDuration;
                expSlider.value = Mathf.Lerp(startPoint, endPoint, curve.Evaluate(percentageComplete));
                yield return null;
            }
        }

    
        private void LoadSavedValue()
        {
            expSlider.value = 0;
        }
    }
}