using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawnController : MonoBehaviour {
    public Camera MainCamera;
    Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
        MainCamera = Camera.main;
        rigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if (Input.GetAxis("Vertical") != 0)
        {
            rigid.AddForce(new Vector2(0.0f, Input.GetAxis("Vertical")));
            Debug.Log(Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            rigid.AddForce(new Vector2(Input.GetAxis("Horizontal"),0.0f));
            Debug.Log(Input.GetAxis("Horizontal"));
        }
    }
}
