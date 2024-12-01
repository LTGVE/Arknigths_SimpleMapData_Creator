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
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeTile(TileType type) {
        switch (type) {
            case TileType.HIGHLAND:
                {
                    tile.tileKey = "tile_wall";
                    tile.heightType = "HIGHLAND";
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                    this.GetComponent<MeshRenderer>().material=MapInit._this.CommonMaterial;
                    break;
                }
            case TileType.FOBBIDEN: {
                    tile.tileKey = "tile_fobbiden";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.FobbidenMaterial;
                    this.transform.position = new Vector3(transform.position.x, transform.position.y,-0.5f);
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    tile.heightType = "HIGHLAND";
                    break;
                }
            case TileType.ROAD: {
                    tile.tileKey = "tile_road";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.CommonMaterial;
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    tile.heightType = "LOWLAND";
                    break;
                }
            case TileType.START:
                {
                    tile.tileKey = "tile_start";
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.CommonMaterial;
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                    tile.heightType = "LOWLAND";
                    break;
                }
            case TileType.END:
                {
                    tile.tileKey = "tile_end";
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.CommonMaterial;
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                    tile.heightType = "LOWLAND";
                    break;
                }
            case TileType.FLY_START:
                {
                    tile.tileKey = "tile_fly";
                    this.GetComponent<MeshRenderer>().material = MapInit._this.FobbidenMaterial;
                    this.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
                    mapTile.gameObject.name = $"{tile.tileKey}#({mapTile.MapPos.y},{mapTile.MapPos.x})";
                    tile.heightType = "HIGHLAND";
                    break;
                }
        }
    }
}
