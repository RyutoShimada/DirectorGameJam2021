using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] GameObject m_panel = null;

    [SerializeField ]float fadeSpeed = 0.02f;
    float red, green, blue, alfa;

    bool isFadeOut = false;
    bool isFadeIn = false;

    Image fadeImage;

    //public void LoadTitle()
    //{
    //    EditorSceneManager.LoadScene("MasterTitliScene");

    //    if (EditorSceneManager.GetActiveScene().name == "MasterTitleScene")
    //    {
    //        // MasterGameScene へロード
    //        EditorSceneManager.LoadScene("MasterGameScene");
    //    }
    //    else
    //    {
    //        // MasterTitleScene へロード
    //        EditorSceneManager.LoadScene("MasterTitleScene");

    //    }
    //}
    public void LoadTitle()
    {
        StartCoroutine(StartLoadTilteScene());
    }

    public void LoadGame()
    {
        StartCoroutine(StartLoadGameScene());
    }



    private void Start()
    {
        fadeImage = m_panel.GetComponent<Image>();

        alfa = fadeImage.color.a;

        StartFadeIn();
    }

    void StartFadeOut()
    {
        fadeImage.enabled = true;
        alfa += fadeSpeed;
        SetAlpha();
        if (alfa >= 1)
        {
            isFadeOut = false;
        }
    }

    void StartFadeIn()
    {
        alfa -= fadeSpeed;
        SetAlpha();
        if (alfa <= 0)
        {
            isFadeIn = false;
            fadeImage.enabled = false;
        }
    }

    private void Update()
    {
        if (isFadeIn)
        {
            StartFadeIn();
        }
        if (isFadeOut)
        {
            StartFadeOut();
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(0, 0, 0, alfa);
    }

    IEnumerator StartLoadGameScene()
    {
        m_panel.gameObject.SetActive(true);
        //StartFadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MasterGameScene");
    }

    IEnumerator StartLoadTilteScene()
    {
        m_panel.gameObject.SetActive(true);
        StartFadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MasterTitleScene");
    }


}
