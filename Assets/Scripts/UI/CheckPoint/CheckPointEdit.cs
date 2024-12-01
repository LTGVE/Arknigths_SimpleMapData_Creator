using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Close() {
    gameObject.SetActive(false);
    }
    public void Show(CheckPoint checkPoint) {
    
    
    }
}
