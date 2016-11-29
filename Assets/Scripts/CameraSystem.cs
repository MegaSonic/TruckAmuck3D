using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour {

    [SerializeField]
    GameObject[] spectatorCameras;

    private void Awake()
    {
        foreach(GameObject c in spectatorCameras)
        {
            c.SetActive(false);
        }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
