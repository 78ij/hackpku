using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(load());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator load()
    {
        yield return new WaitForSeconds(4f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
    }
}
