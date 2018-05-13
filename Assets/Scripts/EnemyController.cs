using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    GameObject player;
    int health = 3;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(attack());
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.GetComponent<pawnController>().isattacking)
                health -= 1;
        }
    }
        // Update is called once per frame
        void Update () {
        if (health <= 0)
            StartCoroutine(death());
	}
    IEnumerator attack()
    {
        while (true)
        {
            float distance = Mathf.Abs((player.transform.position - transform.position).magnitude);
            if (distance <= 4)
            {
                Debug.Log("distance= " + distance.ToString());
                if (Random.Range(0f, 1f) > 0.9f)
                {
                    GetComponent<Animator>().SetTrigger("att");
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
        
    }
    IEnumerator death()
    {
        GetComponent<Animator>().SetTrigger("walk");
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);

    }
}
