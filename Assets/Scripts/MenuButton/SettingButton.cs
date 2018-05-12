using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public Button start;
    public Animator anim;
    // Use this for initialization
    void Start()
    {
        start.onClick.AddListener(startbuttonclick);
    }

    public void startbuttonclick()
    {
        StartCoroutine(startgame());
    }
    IEnumerator startgame()
    {
        anim.SetTrigger("trans");
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(2);
    }
}
