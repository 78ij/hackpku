using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitButton : MonoBehaviour {
    public Button quit;
    // Use this for initialization
    void Start() {
        quit.onClick.AddListener(quitbuttonclick);
    }

    // Update is called once per frame
    public void quitbuttonclick()
    {
    }
}
