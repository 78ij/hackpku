using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAdj : MonoBehaviour {
    public List<GameObject> right;
    List<Vector3> offseta = new List<Vector3>();
    public List<GameObject> left;
    List<Vector3> offsetb = new List<Vector3>();

    Vector2 mouse;
    // Use this for initialization
    void Start () {
        mouse = Input.mousePosition;
        foreach (var a in right)
        {
            offseta.Add(a.transform.position);
        }
        foreach (var b in left)
        {
            offsetb.Add(b.transform.position);
        }
    }
	
	// Update is called once per frame
	void Update () {
        mouse = Input.mousePosition;
        for(int i = 0;i < right.Count;i++)
        {
            var a = right[i];
            a.transform.position = offseta[i] + new Vector3(mouse.x,mouse.y,0) / 4000;
        }
        for (int i = 0; i < left.Count; i++)
        {
            var b = left[i];
            b.transform.position = offsetb[i] - new Vector3(mouse.x, mouse.y, 0) / 4000;
        }
    }
}
