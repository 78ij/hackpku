using UnityEngine;
using System.Collections;
using Pathfinding;

public class pathfind : MonoBehaviour
{
    //目标位置;  
    Vector3 targetPosition;
    AstarPath p;
    Seeker seeker;
    GameObject player;
    CharacterController characterController;

    //计算出来的路线;  
    Path path;

    //移动速度;  
    float playerMoveSpeed = 10f;

    //当前点  
    int currentWayPoint = 0;

    bool stopMove = true;

    //Player中心点;  
    float playerCenterY = 1.0f;


    // Use this for initialization  
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("A*").GetComponent<AstarPath>();
        p.Scan();
        player = GameObject.FindGameObjectWithTag("Player");
        seeker = GetComponent<Seeker>();
        characterController = GetComponent<CharacterController>();
        playerCenterY = transform.localPosition.y;
        targetPosition = player.transform.position;
        targetPosition += new Vector3(0, 0, 1);

        Debug.Log("targetPosition=" + targetPosition);
        StartCoroutine(finding());
    }

    //寻路结束;  
    public void OnPathComplete(Path p)
    {
        Debug.Log("OnPathComplete error = " + p.error);

        if (!p.error)
        {
            currentWayPoint = 0;
            path = p;
            stopMove = false;
        }

        for (int index = 0; index < path.vectorPath.Count; index++)
        {
            Debug.Log("path.vectorPath[" + index + "]=" + path.vectorPath[index]);
        }
    }

    // Update is called once per frame  
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(":ddddd");
            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                return;
            }
            if (!hit.transform)
            {
                return;
            }
            targetPosition = hit.point;// new Vector3(hit.point.x, transform.localPosition.y, hit.point.z);  

            Debug.Log("targetPosition=" + targetPosition);

            seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        }
    }

    void FixedUpdate()
    {
        if (path == null || stopMove)
        {
            return;
        }

        //根据Player当前位置和 下一个寻路点的位置，计算方向;  
        Vector3 currentWayPointV = new Vector3(path.vectorPath[currentWayPoint].x, path.vectorPath[currentWayPoint].y + playerCenterY,-2);
        Vector3 dir = (currentWayPointV - transform.position).normalized;

        //计算这一帧要朝着 dir方向 移动多少距离;  
        dir *= playerMoveSpeed * Time.fixedDeltaTime;

        //计算加上这一帧的位移，是不是会超过下一个节点;  
        float offset = Vector3.Distance(transform.localPosition, currentWayPointV);

        if (offset < 0.1f)
        {
            transform.localPosition = currentWayPointV;

            currentWayPoint++;

            if (currentWayPoint == path.vectorPath.Count)
            {
                stopMove = true;

                currentWayPoint = 0;
                path = null;
            }
        }
        else
        {
            if (dir.magnitude > offset)
            {
                Vector3 tmpV3 = dir * (offset / dir.magnitude);
                dir = tmpV3;

                currentWayPoint++;

                if (currentWayPoint == path.vectorPath.Count)
                {
                    stopMove = true;

                    currentWayPoint = 0;
                    path = null;
                }
            }
            transform.localPosition += dir;
            
        }
    }
    IEnumerator finding()
    {

        //Debug.Log("targetPosition=" + targetPosition);

        while (true) {
                    targetPosition = player.transform.position;
        Debug.Log(targetPosition);
            targetPosition += new Vector3(0, 1, 0);
            seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            yield return new WaitForSeconds(1.0f);
        }

    }
}