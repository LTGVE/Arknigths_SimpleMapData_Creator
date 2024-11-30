using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public MapMesh mesh;
    public Vector2 MapPos;
    // Start is called before the first frame update
    public void init(Vector2 MapPos) { 
        this.MapPos= MapPos;
    }
    public void init2(MapMesh mapMesh) {
    this.mesh=mapMesh;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
