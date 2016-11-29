using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    #region vars

    public static GameManager instance = null;

    #endregion

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    // Use this for initialization
    void Start () {

        SceneManager.LoadScene(1);

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("DEBUG_CAMERA"))
        {
            Debug.Log("Switching Camera");
            //switch camera here 
        }
    }

    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }


}
