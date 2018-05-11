using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class VisionController : MonoBehaviour
{
    public Mesh mesh;
    public int BorderDensity;
    MapGenerator mapg;
    MeshRenderer renderer;
    MeshFilter filter;
    GameObject pawn;
    Material mat;
    int[,] tiles;
    // Use this for initialization
    void Start()
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        mesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        int mask = LayerMask.GetMask("Wall");
        var blocks = Physics2D.CircleCastAll(this.transform.position, 15.0f, Vector2.zero, mask);
        //获取阻挡物上的点
        var points = blocks
            .Select(rh => rh.collider as BoxCollider2D)
            .Where(bx => bx != null)
            .SelectMany(bx =>
            {
                Vector2[] pts = new Vector2[8];
                var temp = bx.size;
                temp.y = -temp.y;
                //转换到世界坐标
                pts[0] = bx.transform.TransformPoint(bx.offset + bx.size / 2.001f);
                pts[1] = bx.transform.TransformPoint(bx.offset - bx.size / 2.001f);
                pts[2] = bx.transform.TransformPoint(bx.offset + temp / 2.001f);
                pts[3] = bx.transform.TransformPoint(bx.offset - temp / 2.001f);
                pts[4] = bx.transform.TransformPoint(bx.offset + bx.size / 1.999f);
                pts[5] = bx.transform.TransformPoint(bx.offset - bx.size / 1.999f);
                pts[6] = bx.transform.TransformPoint(bx.offset + temp / 1.999f);
                pts[7] = bx.transform.TransformPoint(bx.offset - temp / 1.999f);
                return pts;
            });
        //填充边界点，BorderDentisy表示边界一共有多少点。值越大，阴影越圆滑。
        Vector2[] borderPoints = new Vector2[BorderDensity];
        float deltaAngle = 360.0f / BorderDensity;
        for (int i = 0; i < BorderDensity; i++)
        {
            borderPoints[i] = transform.position + Quaternion.Euler(0, 0, i * deltaAngle) * Vector2.up * 15.0f;
        }
        //向所有点投影。留下最近的交点。
        points = points.Concat(borderPoints).Select(
                pt => {
                    var t = Physics2D.Raycast(transform.position, pt - (Vector2)transform.position, 115.0f, mask);
                    if (t.collider == null)
                        return (Vector2)transform.position + (pt - (Vector2)transform.position).normalized * 15.0f;
                    else
                        return t.point;
                }
            );
        //转回local space并且按角度排序
        var orderedpts = points
            .Select(pt => transform.InverseTransformPoint(pt))
            .OrderByDescending(
                pt => {
                    var sign = Mathf.Sign(Vector2.up.x * pt.y - Vector2.up.y * pt.x);
                    return Vector2.Angle(Vector2.up, pt) * sign;        //Angle只会返回正值，要角度排序必须区分正负
                }
            );

        var zeropt = new Vector3[1];            //记得加入原点
        zeropt[0] = new Vector3(0,0,-1);
        var verticesArray = zeropt.Concat(orderedpts).ToArray();
        Debug.Log(verticesArray.Length);

        int[] triangles = new int[(verticesArray.Length - 1) * 3];
        for (int i = 0; i < verticesArray.Length - 1; i++)
        {        //相邻两点和原点构成一个三角形。
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = (i + 1) % (verticesArray.Length - 1) + 1;    //让最后一个三角形的最后一个顶点为1。
        }

        mesh.Clear();
        mesh.vertices = verticesArray;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        filter.mesh = mesh;
    }
}
