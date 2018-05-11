using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class MapGenerator : MonoBehaviour {
    public int width;
    public int height;
    public GameObject wall;
    public GameObject floor;
    Collider2D col;
    // Use this for initialization
    void Start () {
        float scale = floor.transform.localScale.x *
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
        string retinfo = sr.ReadToEnd();//获得字符串
        Debug.Log(retinfo);
        int[,] tiles = new int[30, 20];
        col = wall.GetComponent<Collider2D>();
        Debug.Log(retinfo);
        Debug.Log(scale);
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j <= 30; j++)
            {
                if (j == 30) continue;
                else if (retinfo[i * 20 + j] == ' ' ||
                    retinfo[i * 20 + j] == '>' ||
                    retinfo[i * 20 + j] == '<' ||
                    retinfo[i * 20 + j] == '+')
                {
                    tiles[j, i] = 0;
                    Instantiate(floor, new Vector3(j - 15, i - 10, 0) * scale , new Quaternion());
                }
                else
                {
                    tiles[j, i] = 1;
                    Instantiate(wall, new Vector3(j - 15, i - 10, 0) * scale , new Quaternion());

                }

            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
