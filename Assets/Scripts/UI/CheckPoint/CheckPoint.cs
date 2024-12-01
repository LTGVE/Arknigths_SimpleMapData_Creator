using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public float X;
    public float Y;
    public float ReachOffsetX;
    public float ReachOffsetY;
    public CheckPointType type;
    public void init(float X,float Y,float ReachOffsetX,float ReachOffsetY,CheckPointType type) {
        this.X = X;
        this.Y = Y;
        this.ReachOffsetX = ReachOffsetX;
        this.ReachOffsetY = ReachOffsetY;
        this.type = type;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
