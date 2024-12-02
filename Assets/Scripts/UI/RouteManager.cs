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
    public static route selectedRoute;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedRoute!=MapInit._this.LevelData.routes[int.Parse(ListDropdown.options[ListDropdown.value].text)]) {
            selectedRoute = MapInit._this.LevelData.routes[int.Parse(ListDropdown.options[ListDropdown.value].text)];
        }
    }
    public void AddNewRoute()
    {
        route route = new route();
        MapInit._this.LevelData.routes.Add(route);
        ListDropdown.options.Add(new Dropdown.OptionData( (MapInit._this.LevelData.routes.Count - 1).ToString()));
    }
}
