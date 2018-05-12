using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraAdj : MonoBehaviour {
    Camera camera;
	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        camera.aspect = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
