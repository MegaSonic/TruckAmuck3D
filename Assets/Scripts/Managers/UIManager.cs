using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {


    #region vars

    [SerializeField]
    GameObject mainCanvasGO;

    [SerializeField]
    float fadeInFactor = 1f;

    private CanvasGroup menuCG;

    bool fadingIn = true;

    #endregion  

    private void Awake()
    {
        menuCG = mainCanvasGO.GetComponent<CanvasGroup>();

        menuCG.alpha = 0f;

        StartCoroutine(FadeInMainMenu());
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Submit") && !fadingIn)
        {
            fadingIn = true;
            Debug.Log("bam");
            StartCoroutine(FadeOutToLevelOne());
        }
	
	}

    IEnumerator FadeInMainMenu()
    {

        while (menuCG.alpha < 1)
        {
            menuCG.alpha += Time.deltaTime / fadeInFactor;
            yield return null;
        }

        fadingIn = false;
        yield return null;

    }

    IEnumerator FadeOutToLevelOne()
    {
        while (menuCG.alpha > 0)
        {
            menuCG.alpha -= Time.deltaTime / fadeInFactor;
            yield return null;
        }

        GameManager.instance.LoadLevel(2);
        yield return null;
    }
}

