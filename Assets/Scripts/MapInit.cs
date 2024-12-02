using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapInit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Material CommonMaterial;
    [SerializeField]
    public Material FobbidenMaterial;
    [SerializeField]
    public Material endMaterial;
    [SerializeField]
    public Material StartMaterial;
    [SerializeField]
    public Material F_STARTMaterial;
    [SerializeField]
    public GameObject Tile;
    [SerializeField]
    public GameObject Fobbiden;
    [SerializeField]
    public GameObject Light;
    [SerializeField]
    public LevelData LevelData;
    public bool isCreatedScene=false;
    public static MapInit _this;
    public Dropdown TileType;
    public GameObject mainBATTLE;
    void Start()
    {
        _this = this;
        TileType=transform.Find("TileType").GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTile() {
        LevelData level=new LevelData();
        if (mainBATTLE != null) {
                Destroy(mainBATTLE);
        }
        GameObject BATTLE = new GameObject() { name = "BATTLE" };
        mainBATTLE = BATTLE;
        GameObject Tile = new GameObject() { name = "Tile" };
        Tile.transform.parent = BATTLE.transform;
        GameObject Mesh = new GameObject() { name = "Mesh" };
        Mesh.transform.position = new Vector3(0,0,0);
        Mesh.transform.parent = BATTLE.transform;
        var X = int.Parse(_this.transform.Find("X").GetComponent<InputField>().text);
        var Y = int.Parse(_this.transform.Find("Y").GetComponent<InputField>().text);
        BATTLE.transform.position = new Vector3(-X/2 +1, -Y/2, 0);
       // Tile.transform.localPosition = new Vector3(-BATTLE.transform.position.x, -BATTLE.transform.position.y, 0);
        mapdata Mapdata = new mapdata();
        List<List<int>> map = new List<List<int>>();
        int count = 0;
        var tiles = new List<tile>();
        for (int y = 0; y < Y; y++)
        {
            List<int> list = new List<int>();
            for (int x = 0; x < X; x++)
            {
                GameObject tile =Instantiate(Fobbiden);
                tile.transform.parent = Mesh.transform;
                tile.transform.localPosition = new Vector3(x,y); 
                GameObject tileKey = new GameObject() { name = $"tile_fobbiden#({y},{x})" };
                tileKey.transform.parent = Tile.transform;
                tileKey.transform.localPosition = new Vector3(tile.transform.localPosition.x, tile.transform.localPosition.y, -0.5f);
                tileKey.AddComponent<BoxCollider2D>();
                 tileKey.AddComponent<MapTile>().init(new Vector2(x,y));
                var maptile=tileKey.GetComponent<MapTile>();
                var tile1 = new tile()
                { 
                    tileKey = "tile_fobbiden",
                    heightType = "HIGHLAND",
                    buildableType = "NONE",
                    passableMask = "FLY_ONLY",
                    playerSideMask = "ALL"
                };
                tiles.Add(tile1);
                tile.AddComponent<MapMesh>().init(tile1, maptile); 
                list.Add(count);
                count += 1;
            }
            map.Add(list);
        }
        map.Reverse();
        Mapdata.map = map;
        Mapdata.tiles = tiles;
        level.mapdata = Mapdata;
    }

    public void SpawnScene() {
        var name = _this.transform.Find("SceneName").GetComponent<InputField>().text;
        if (name=="") { return; }
        if (!isCreatedScene)
        {
            var scene = SceneManager.CreateScene(name);
            SceneManager.SetActiveScene(scene);
            isCreatedScene = true;
            Instantiate(Light);
        }
        else {
            var scene = SceneManager.GetActiveScene();
            if (scene.name!="Main") {
                SceneManager.UnloadSceneAsync(scene);
                isCreatedScene = false;
                SpawnScene();
            }
        }
    }
}
