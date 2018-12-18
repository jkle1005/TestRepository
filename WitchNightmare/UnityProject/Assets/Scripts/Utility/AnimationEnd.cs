using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationEnd : MonoBehaviour 
{
    public Image blur;
    public Image back;
    public float fadetime;

    public void End()
    {
        gameObject.SetActive(false);
    }
    
    public void FadeInBlur()
    {
        StartCoroutine(fadeInBlur());
    }

    private IEnumerator fadeInBlur()
    {
        Material mat = blur.material;
        float time_now = 0f;

        while(true)
        {
            float r = time_now / fadetime;
                
            mat.SetFloat("Distortion", 1.5f * r);
            back.color = new Color(0f, 0f, 0f, 0.5f * r);
            time_now += Time.deltaTime;

            if (r >= 1f)
                break;

            yield return null;
        }
    }

    public void FadeOutBlur()
    {
        StartCoroutine(fadeOutBlur());
    }

    private IEnumerator fadeOutBlur()
    {
        Material mat = blur.material;
        float time_now = 0f;

        while (true)
        {
            float r = time_now / fadetime;

            mat.SetFloat("Distortion", 1.5f - (1.5f * r));
            back.color = new Color(0f, 0f, 0f, 0.5f - (0.5f * r));
            time_now += Time.deltaTime;

            if (r >= 1f)
                break;

            yield return null;
        }
    }
}
