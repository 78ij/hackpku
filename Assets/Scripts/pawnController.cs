using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pawnController : MonoBehaviour {
    public Camera MainCamera;
    Rigidbody2D rigid;
    GameObject generator;
    public float scale;
	// Use this for initialization
	void Start () {
        generator = GameObject.FindGameObjectWithTag("generator");
        MainCamera = Camera.main;
        rigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        generator.transform.position = new Vector3(transform.position.x,transform.position.y,0);

        //if (Input.GetAxis("Vertical") != 0)
        //{
        //    rigid.AddForce(new Vector2(0.0f, Input.GetAxis("Vertical")));
        //    if (Input.GetKey("left shift"))
        //    {
        //        rigid.AddForce(new Vector2(0.0f, 1.5f * Input.GetAxis("Vertical")));

        //    }
        //}
        //if (Input.GetAxis("Horizontal") != 0)
        //{
        //    rigid.AddForce(new Vector2(Input.GetAxis("Horizontal"),0.0f));
        //    if (Input.GetKey("left shift"))
        //    {
        //        rigid.AddForce(new Vector2(1.5f * Input.GetAxis("Horizontal"), 0.0f));
        //    }
        //}
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (Input.GetKey("left shift"))
        {
            scale =  4.5f;
        }
        rigid.velocity =  scale * (new Vector2(x, y));

    }

}
