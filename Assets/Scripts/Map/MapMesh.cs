using Assets.Scripts.Manager;
using UnityEngine;

public class MapMesh : MonoBehaviour
{
    // Start is called before the first frame update
    public tile tile;
    public MapTile mapTile;
    public void init(tile FatherTile,MapTile mapTile) {
        tile = FatherTile;
        this.mapTile = mapTile;
        mapTile.init2(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeTile(int Type) {
        switch (Type) {
            case (int)TileType.HIGHLAND:
                {
                    tile.tileKey = "tile_wall";
                    tile.heightType = "HIGHLAND";
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                    mapTile.gameObject.transform.position = new Vector3(mapTile.gameObject.transform.position.x, mapTile.gameObject.transform.position.y, -0.5f);
                    mapTile.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    this.GetComponent<MeshRenderer>().material=MapInit._this.CommonMaterial;
                    break;
                }
            case (int)TileType.FOBBIDEN: {
                    tile.tileKey = "tile_fobbiden";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.FobbidenMaterial;
                    this.transform.position = new Vector3(transform.position.x, transform.position.y,-0.5f);
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    mapTile.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    mapTile.gameObject.transform.position = new Vector3(mapTile.gameObject.transform.position.x, mapTile.gameObject.transform.position.y, -0.5f);
                    tile.heightType = "HIGHLAND";
                    break;
                }
            case (int)TileType.ROAD: {
                    tile.tileKey = "tile_road";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.CommonMaterial;
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                    mapTile.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    tile.heightType = "LOWLAND";
                    break;
                }
            case (int)TileType.START:
                {
                    tile.tileKey = "tile_start";
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.StartMaterial;
                    mapTile.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                    this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                    tile.heightType = "LOWLAND";
                    break;
                }
            case (int)TileType.END:
                {
                    tile.tileKey = "tile_end";
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.endMaterial;
                    mapTile.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                    this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                    tile.heightType = "LOWLAND";
                    break;
                }
            case (int)TileType.FLY_START:
                {
                    tile.tileKey = "tile_fly";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.F_STARTMaterial;
                    mapTile.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    tile.heightType = "HIGHLAND";
                    break;
                }
        }
    }
}
