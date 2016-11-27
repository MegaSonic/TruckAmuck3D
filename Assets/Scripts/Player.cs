using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Rigidbody rigid;

    public float speed;


	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        /*
        // we need some axis derived from camera but aligned with floor plane
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        forward.y = 0f;
        forward = forward.normalized;
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);
        */
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0.0f, v);

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        movement = movement.normalized;
        rigid.velocity = movement * speed * Time.deltaTime;

        /*
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                rigid.velocity = new Vector3(speed, 0, 0);
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                rigid.velocity = new Vector3(speed, 0, 0);
            }
            else
            {

            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                rigid.velocity = new Vector3(-speed, 0, 0);
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                rigid.velocity = new Vector3(-speed, 0, 0);
            }
            else
            {

            }
        }
        else
        {
            if (Input.GetAxis("Vertical") < 0)
            {

            }
            else if (Input.GetAxis("Vertical") > 0)
            {

            }
            else
            {

            }
        }
        */
        
    }
}
