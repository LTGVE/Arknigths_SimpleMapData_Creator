using Assets.Scripts.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouteManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Dropdown ListDropdown;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void AddNewRoute()
    {
        route route = new route();
        MapInit._this.LevelData.routes.Add(route);
        ListDropdown.AddOptions(new List<Dropdown.OptionData>() { new Dropdown.OptionData() { text = (MapInit._this.LevelData.routes.Count - 1).ToString() } });
    }
}
