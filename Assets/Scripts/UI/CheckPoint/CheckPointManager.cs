using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckPointManager : MonoBehaviour
{
    public int routeIndex = 0;
    public GameObject CheckPointPrefab;
    public static CheckPointManager Instance;
    List<GameObject> checkpoints = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        CheckPointPrefab=transform.Find("cp").gameObject;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (RouteManager.selectedRoute.checkpoints.Count!=getCheckPointCount()) {
            for (int i = 0; i < checkpoints.Count; i++)
            {
                int x = (int)checkpoints[i].GetComponent<CheckPoint>().X;
                int y = (int)checkpoints[i].GetComponent<CheckPoint>().Y;
                float reachoffsetX = checkpoints[i].GetComponent<CheckPoint>().ReachOffsetX;
                float reachoffsetY = checkpoints[i].GetComponent<CheckPoint>().ReachOffsetY;
                position pos = new position(x, y);
                reachOffset reachOffset = new reachOffset(reachoffsetX, reachoffsetY);
                RouteManager.selectedRoute.checkpoints.Add(new CPoint(checkpoints[i].GetComponent<CheckPoint>().type.ToString(), 0, pos, reachOffset));
            }
        }
    }
    public void AddCheckPoint(Vector2 pos) { 
        GameObject cp = Instantiate(CheckPointPrefab, CheckPointPrefab.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);
        cp.AddComponent<CheckPoint>();
        cp.GetComponent<CheckPoint>().init(checkpoints.Count,pos,CheckPointType.MOVE);
        checkpoints.Add(cp);
        for (int i =0;i<checkpoints.Count;i++ ) {
            checkpoints[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(checkpoints[i].GetComponent<RectTransform>().anchoredPosition.x, checkpoints[i].GetComponent<RectTransform>().anchoredPosition.y-40,0);
        }
    }
    public int getCheckPointCount() {
        int count = 0;
        foreach (var cp in checkpoints) {
         count += cp.GetComponent<CheckPoint>().type.ToString()!="START"||cp.GetComponent<CheckPoint>().type.ToString()!="END"? 1:0;
        }
        return count;
    }
    public void AddCheckPoint(MapTile tile)
    {
        GameObject cp = Instantiate(CheckPointPrefab, CheckPointPrefab.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);
        cp.AddComponent<CheckPoint>();
        checkpoints.Add(cp);
        for (int i = 0; i < checkpoints.Count; i++)
        {
            checkpoints[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(checkpoints[i].GetComponent<RectTransform>().anchoredPosition.x, checkpoints[i].GetComponent<RectTransform>().anchoredPosition.y - 40, 0);
        }
    }
    public void reFlushCheckPoint() {
        for (int i = 0; i < checkpoints.Count; i++)
        {
            checkpoints[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(checkpoints[i].GetComponent<RectTransform>().anchoredPosition.x, checkpoints[i].GetComponent<RectTransform>().anchoredPosition.y - 40, 0);
        }
    }
    public void CleanAllCheckPoint() {
        foreach (var cp in checkpoints) {
            Destroy(cp);
            checkpoints.Clear();
        }
    }
}
