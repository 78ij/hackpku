using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pawnController : MonoBehaviour {
    public float hp;
    public float mp;
    public Animator anim;
    public Camera MainCamera;
    Rigidbody2D rigid;
    GameObject generator;
    Slider health;
    Slider vitality;
    public float scale;
    public bool isattacking;
	// Use this for initialization
	void Start () {
        hp = 100;
        mp = 100;
        anim = gameObject.GetComponent<Animator>();
        generator = GameObject.FindGameObjectWithTag("generator");
        MainCamera = Camera.main;
        rigid = this.GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            hp -= 5;
        }
    }
    void FixedUpdate()
    {
        if (health == null || vitality == null)
        {
            health = GameObject.FindGameObjectWithTag("hp").GetComponent<Slider>();
            vitality = GameObject.FindGameObjectWithTag("mp").GetComponent<Slider>();
        }
        vitality.value = mp / 100f;
        health.value = hp / 100f;
        if (mp < 100) mp += 0.1f;

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
        //    }s
        //}
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (Input.GetKey("left shift"))
        {
            scale =  4.5f;
        }
        rigid.velocity =  scale * (new Vector2(x, y));
        if (rigid.velocity.magnitude >= 0.2f)
        {
            anim.SetBool("walk", true);
            anim.SetBool("idle", false);

        }
        if (rigid.velocity.magnitude < 0.2f)
        {
            anim.SetBool("idle", true);
            anim.SetBool("walk", false);


        }
        if (Input.GetButtonUp("Fire3"))
        {
            if(mp >= 10)
            {
                anim.SetBool("walk", false);
                anim.SetBool("idle", false);
                anim.SetTrigger("att");
                StartCoroutine(atta());
                mp -= 10;
            }

        }
        if (rigid.velocity.x < 0) transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        if (rigid.velocity.x > 0) transform.localScale = new Vector3(0.5f, 0.5f, 1);

    }
    IEnumerator atta()
    {
        isattacking = true;
        yield return new WaitForSeconds(0.25f);
    }
}
