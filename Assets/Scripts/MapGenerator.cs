using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class MapGenerator : MonoBehaviour {
    public int width;
    public int height;
    public GameObject[] floors = new GameObject[4];
    public GameObject[] walls = new GameObject[3];
    public GameObject wall;
    public GameObject unused;
    public GameObject floor;
    public GameObject pawn;
    public int[,] tiles;
    Collider2D col;
    public float scale;
    // Use this for initialization
    void Start () {
        tiles = new int[50, 50];
        scale = floor.transform.localScale.x *
            floor.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        scale -= 0.02f;
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.FileName = "D:\\hackpku\\Assets\\Scripts\\test.exe";
        p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
        p.StartInfo.CreateNoWindow = true;//不显示程序窗口
        p.Start();//启动程序
        StreamReader sr = p.StandardOutput;//将输出内容返回
        List<string> strings = new List<string>();
        string str;
        while((str = sr.ReadLine()) != null){
            strings.Add(str);
        }
        Debug.Log(strings[0].Length);
        col = wall.GetComponent<Collider2D>();
        //Debug.Log(retinfo);
        //Debug.Log(scale);
        bool isinstaned = false;
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                 if (strings[i][j] == ' ' ||

                   strings[i][j] == '>' ||
                   strings[i][j] == '<' ||
                   strings[i][j] == '+')
                {
                    tiles[i,j] = 0;
                     if(Random.Range(0,1) + 1.0 / 500 * (i * 50 + j) > 1 && !isinstaned)
                     {
                        Instantiate(pawn, new Vector3(i - 24.5f, j - 24.5f, -2) * scale, new Quaternion());
                        isinstaned = true;
                     }
                    ranfloor();
                     Instantiate(floor, new Vector3(i - 24.5f, j - 24.5f, 0) * scale , new Quaternion());
                }
                else if(strings[i][j] == '.')
                {
                    Instantiate(unused, new Vector3(i - 24.5f, j - 24.5f, 0) * scale, new Quaternion());

                }
                else
                {
                    tiles[j, i] = 1;
                    Instantiate(wall, new Vector3(i - 24.5f, j - 24.5f, 0) * scale , new Quaternion());
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    void ranfloor()
    {
        int i = Random.Range(0, 3);
        floor = floors[i];
    }
    void ranwall()
    {
        int i = Random.Range(0, 2);
        wall = walls[i];
    }
 }
