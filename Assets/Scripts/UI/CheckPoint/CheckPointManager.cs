using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
