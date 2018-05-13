using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingWindowController : MonoBehaviour {
    public GameObject PopUpWindow;
    float master;
    float sfx;
    float bgm;
    public Slider m;
    public Slider s;
    public Slider b;
    public AudioSource bg;
    public Animator animbg;
    public Animator animpop;
    public Button back;
    public Button yes;
    public Button no;
    public Button Reset;
    // Use this for initialization
    void Start()
    {
        back.onClick.AddListener(Back);
        yes.onClick.AddListener(sure);
        no.onClick.AddListener(not);
        if ((master = PlayerPrefs.GetInt("master")) == 0)
        {
            PlayerPrefs.SetInt("master", 80);
            master = 80;
        }
        if ((sfx = PlayerPrefs.GetInt("sfx")) == 0)
        {
            PlayerPrefs.SetInt("sfx", 80);
            sfx = 80;
        }
        if ((bgm = PlayerPrefs.GetInt("bgm")) == 0)
        {
            PlayerPrefs.SetInt("bgm", 80);
            bgm = 80;
        }
        m.value = master / 100.0f * 20;
        s.value = sfx / 100.0f * 20;
        b.value = bgm / 100.0f * 20;

        PopUpWindow.SetActive(false);

    }
	// Update is called once per frame
	void Update () {
        bgm = b.value / 20.0f * 100.0f;
        master = m.value / 20.0f * 100.0f;
        sfx = s.value / 20.0f * 100.0f;
        if (bgm == 0) bgm++;
        if (sfx == 0) sfx++;
        if (master == 0) master++;
        PlayerPrefs.SetInt("bgm", 80);
        PlayerPrefs.SetInt("master", 80);
        PlayerPrefs.SetInt("sfx", 80);
        bg.volume = master / 100.0f * bgm / 100.0f;

    }
    void Back()
    {
        PopUpWindow.SetActive(true);
        animpop.SetTrigger("Popup");
    }
    void sure()
    {
        StartCoroutine(goback());
    }
    void not()
    {
        //Debug.Log("aaaa");
        StartCoroutine(reject());
    }
    IEnumerator reject()
    {
        animpop.SetTrigger("Popdown");
        yield return new WaitForSeconds(0.5f);
        PopUpWindow.SetActive(false);

    }
    IEnumerator goback()
    {
        animbg.SetTrigger("Glitch");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);

    }
}
