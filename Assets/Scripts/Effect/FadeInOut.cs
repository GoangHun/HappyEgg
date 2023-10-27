using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    [HideInInspector]
    public bool isFading = false;

    private Image fadePanel;

    private void Awake()
    {
        fadePanel = GetComponent<Image>();
    }

    void Start()
    {
        StartFadeIn();
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() 
    {
        isFading = true;
        while (fadePanel.color.a < 1)
        {
            Color color = fadePanel.color;
            color.a += Time.unscaledDeltaTime * fadeSpeed;
            fadePanel.color = color;
            yield return null;
        }
        isFading = false;
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        Debug.Log("start fading");
        isFading = true;
        while (fadePanel.color.a > 0)
        {
            Color color = fadePanel.color;
            color.a -= Time.unscaledDeltaTime * fadeSpeed;
            fadePanel.color = color;
            yield return null;
        }
        isFading = false;
    }
}
