using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public int CheckPointIndex;
    public Text XText;
    public Text YText;
    public Button EditButton;
    public Button DeleteButton;
    public float X;
    public float Y;
    public float ReachOffsetX;
    public float ReachOffsetY;
    public CheckPointType type;
    public bool hasInit = false;
    public void init(int CheckPointIndex,Vector2 pos,CheckPointType type) {
        this.CheckPointIndex = CheckPointIndex;
        this.X = pos.x;
        this.Y = pos.y;
        this.type = type;
        this.XText= transform.Find("X").GetComponent<Text>();
        this.YText= transform.Find("Y").GetComponent<Text>();
        this.EditButton= transform.Find("edit").GetComponent<Button>();
        this.DeleteButton= transform.Find("delete").GetComponent<Button>();
        this.XText.text = X.ToString();
        this.YText.text = Y.ToString();

        hasInit=true;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (hasInit) {
            this.XText.text = X.ToString();
            this.YText.text = Y.ToString();
        }
    }
}
