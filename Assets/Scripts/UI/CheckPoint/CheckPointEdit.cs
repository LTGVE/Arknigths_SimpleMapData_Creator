using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CheckPointEdit : MonoBehaviour
{
    public InputField X;
    public InputField Y;
    public InputField ReachOffsetX;
    public InputField ReachOffsetY;
    public Dropdown Type;
    public static CheckPointEdit instance;
    public CheckPoint checkPoint;
    void Start()
    {
        instance = this;
        X = transform.Find("X").GetComponent<InputField>();
        Y = transform.Find("Y").GetComponent<InputField>();
        ReachOffsetX = transform.Find("RX").GetComponent<InputField>();
        ReachOffsetY = transform.Find("RY").GetComponent<InputField>();
        Type = transform.Find("Type").GetComponent<Dropdown>(); 
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ReachOffsetX.text != "")
        {
            ReachOffsetX.text = Regex.Replace(ReachOffsetX.text, @"[^0-9.]", "");
        }
        else {
            ReachOffsetX.text = "0";
        }
        if (ReachOffsetY.text != "")
        {
            ReachOffsetY.text = Regex.Replace(ReachOffsetY.text, @"[^0-9.]", "");
        }
        else {
            ReachOffsetY.text = "0";
        }
        if (float.Parse(ReachOffsetX.text)>0.5f) {
            ReachOffsetX.text = "0.5";
        }
        if (float.Parse(ReachOffsetY.text)>0.5f) {
            ReachOffsetY.text = "0.5";
        }
    }
    public void Close() {
    gameObject.SetActive(false);
        checkPoint.X = float.Parse(X.text);
        checkPoint.Y = float.Parse(Y.text);
        checkPoint.ReachOffsetX = float.Parse(ReachOffsetX.text);
        checkPoint.ReachOffsetY = float.Parse(ReachOffsetY.text);
        checkPoint.type = (CheckPointType)Type.value;
        checkPoint = null;
    }
    public void Show(CheckPoint checkPoint) {
        gameObject.SetActive(true);
        X.text = checkPoint.X.ToString();
        Y.text = checkPoint.Y.ToString();
        ReachOffsetX.text = checkPoint.ReachOffsetX.ToString();
        ReachOffsetY.text = checkPoint.ReachOffsetY.ToString();
        Type.value = (int)checkPoint.type; 
        this.checkPoint=checkPoint;
    }
}
