using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<GameObject> journal;
     List<Image> jimage = new List<Image>();
    public AudioSource bgm;
    public Button cont;
    public Button set;
    public Button quit;
    public Button jourbt;
    public GameObject Panel;
    public GameObject Panel2;
    public List<GameObject> pause;
    public Button jourbk;
    public Button paus;
    List<Image> images = new List<Image>();
    float oldtime;
    float currenttime;
    public Image image;
    public Text text1;
    public Text text2;
    bool isdisplaying;
    // Use this for initialization
    void Start()
    {
        quit.onClick.AddListener(() => { Application.Quit(); });
        jourbk.onClick.AddListener(() =>
        {
            oldtime = Time.realtimeSinceStartup;
            StartCoroutine(jourback());
        });
        jourbt.onClick.AddListener(() => {
            foreach(var a in journal)
            {
                a.SetActive(true);
            }
            Time.timeScale = 0;
            oldtime = Time.realtimeSinceStartup;
            StartCoroutine(jour());
        });
        paus.onClick.AddListener(() => {

            Panel2.SetActive(true);
            foreach(var a in pause)
            {
                a.SetActive(true);
            }
            Time.timeScale = 0;
            oldtime = Time.realtimeSinceStartup;

            StartCoroutine(pau());
        });
        cont.onClick.AddListener(() => {

            Time.timeScale = 0;
            oldtime = Time.realtimeSinceStartup;

            StartCoroutine(conti());
        });
        foreach (var a in pause)
        {
            images.Add(a.GetComponent<Image>());
            a.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            a.SetActive(false); 
        }
        foreach(var b in journal)
        {
            if(b.GetComponent<Image>() != null)
            {
                jimage.Add(b.GetComponent<Image>());
                b.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            b.SetActive(false);
        }
        image.color = new Color(0, 0, 0, 1);
        oldtime = Time.realtimeSinceStartup;
        isdisplaying = true;
        Time.timeScale = 0;
        //StartCoroutine(Displayjournal());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isdisplaying)
        {
          //  Debug.Log("aaaa");
            isdisplaying = false;
            oldtime = Time.realtimeSinceStartup;
            StartCoroutine(Fadejournal());
        }

    }
    IEnumerator Displayjournal()
    {
        while (true)
        {
            currenttime = Time.realtimeSinceStartup;
         //   Debug.Log(currenttime);
            float alpha = ((currenttime - oldtime) / 2);
          //  Debug.Log(alpha);
            image.color = new Color(0, 0, 0, alpha);
            text1.color = new Color(1, 1, 1, alpha);
            text2.color = new Color(1, 1, 1, alpha);
            if (alpha <= 0)
            {
                alpha = 0;
                break;
            }

            //oldtime = currenttime;
            yield return null;
        }
        yield return null;
    }
    
    IEnumerator Fadejournal()
    {
        while (true)
        {
            currenttime = Time.realtimeSinceStartup;
           // Debug.Log(currenttime);
            float alpha = 1 - ((currenttime - oldtime) / 2);
           // Debug.Log(alpha);
            image.color = new Color(0, 0, 0, alpha);
            text1.color = new Color(1, 1, 1, alpha);
            text2.color = new Color(1, 1, 1, alpha);
            if (alpha <= 0)
            {
                alpha = 0;
                break;
            }

            //oldtime = currenttime;
            yield return null;
        }
        Time.timeScale = 1;
        Panel.SetActive(false);
        bgm.Play();
        yield return null;
    }
    
    IEnumerator pau()
    {
        while (true)
        {
            currenttime = Time.realtimeSinceStartup;
            Debug.Log(currenttime);
            float alpha = ((currenttime - oldtime) * 2);
            Debug.Log(alpha);
            foreach(var a in images)
            {
                a.color = new Color(1, 1, 1, alpha);
                
            }
            Panel2.GetComponent<Image>().color = new Color(0, 0, 0, 0);

            if (alpha >= 1)
            {
                alpha = 1;
                break;
            }

            //oldtime = currenttime;
            yield return null;
        }
    }
    IEnumerator jour()
    {
        while (true)
        {
            currenttime = Time.realtimeSinceStartup;
            //Debug.Log(currenttime);
            float alpha = ((currenttime - oldtime) * 2);
            //Debug.Log(alpha);
            foreach (var a in jimage)
            {
                a.color = new Color(1, 1, 1, alpha);

            }
            if (alpha >= 1)
            {
                alpha = 1;
                break;
            }

            //oldtime = currenttime;
            yield return null;
        }
    }
    IEnumerator jourback()
    {
        while (true)
        {
            currenttime = Time.realtimeSinceStartup;
            //Debug.Log(currenttime);
            float alpha = 1 - ((currenttime - oldtime) * 2);
            //Debug.Log(alpha);
            foreach (var a in jimage)
            {
                a.color = new Color(1, 1, 1, alpha);

            }
            if (alpha <= 0)
            {
                alpha = 0;
                break;
            }

            //oldtime = currenttime;
            yield return null;
        }
        foreach (var a in journal)
        {
            a.SetActive(false);
        }
        Time.timeScale = 1;
        yield return null;
    }
    IEnumerator conti()
    {
        while (true)
        {
            currenttime = Time.realtimeSinceStartup;
            //Debug.Log(currenttime);
            float alpha = 1 - ((currenttime - oldtime) * 2);
           // Debug.Log(alpha);
            foreach (var a in images)
            {
                a.color = new Color(1, 1, 1, alpha);

            }
            Panel2.GetComponent<Image>().color = new Color(0,0,0,0);
            if (alpha <= 0)
            {
                alpha = 0;
                break;
            }
            yield return null;
        }
        foreach (var a in pause)
        {
            a.SetActive(false);
        }
        Panel2.SetActive(false);
        Time.timeScale = 1;
        yield return null;
    }
}