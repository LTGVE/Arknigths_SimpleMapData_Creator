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
    public GameObject Tile;
    [SerializeField]
    public GameObject Fobbiden;
    [SerializeField]
    public GameObject Light;
    [SerializeField]
    public LevelData LevelData;
    public bool isCreatedScene=false;
    public static MapInit _this;
    void Start()
    {
        _this = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTile() {
        GameObject BATTLE = new GameObject() { name="BATTLE"};
        GameObject Tile = new GameObject() { name = "Tile" };
        Tile.transform.parent = BATTLE.transform;
        GameObject Mesh = new GameObject() { name = "Mesh" };
        Mesh.transform.parent = BATTLE.transform;
        var X = int.Parse(_this.transform.Find("X").GetComponent<InputField>().text);
        var Y = int.Parse(_this.transform.Find("Y").GetComponent<InputField>().text);
        mapdata Mapdata = new mapdata();
        List<List<int>> map = new List<List<int>>();
        int count = 0;
        for (int y = 0; y < Y; y++)
        {
            List<int> list = new List<int>();
            for (int x = 0; x < X; x++)
            {
                GameObject tile =Instantiate(Fobbiden);
                tile.transform.parent = Mesh.transform;
                tile.transform.position = new Vector3(x,y); 
                GameObject tileKey = new GameObject() { name = $"tile_fobbiden#({y},{x})" };
                tileKey.transform.parent = Tile.transform;
                tileKey.transform.position = tile.transform.position;
                tileKey.AddComponent<BoxCollider2D>();
                list.Add(count);
                count += 1;
            }
            map.Add(list);
        }
        map.Reverse();
        Mapdata.map = map;
        var tiles = new List<tile>();
        for (int tilec = 0; tilec < count; tilec++)
        {
            var tile = new tile()
            {

                tileKey = "tile_fobbiden",
                heightType = "HIGHLAND",
                buildableType = "NONE",
                passableMask = "FLY_ONLY",
                playerSideMask = "ALL"
            };
                tiles.Add(tile);
        }
        Mapdata.tiles = tiles;
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
