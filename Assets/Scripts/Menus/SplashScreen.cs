using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    // the image you want to fade, assign in inspector
    [SerializeField] private Image myImage;
    private bool loadFinish;
    private bool endLogo;

    private void Start() 
    {
        loadFinish = false;
        endLogo = false;
        myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0f);
        StartCoroutine(FadeEffect());
        Screen.SetResolution(1920, 1080, true);
        Application.targetFrameRate = 60;
        loadFinish = true;
    }

    private void Update() 
    {
        if(loadFinish && endLogo)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private IEnumerator FadeEffect()
    {
        float fadeCount = 0;

        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, fadeCount);
        }

        while (fadeCount > 0.01f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(-0.01f);
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, fadeCount);
        }
        endLogo = true;
    }

}
