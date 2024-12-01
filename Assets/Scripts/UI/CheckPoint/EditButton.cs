using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditButton : MonoBehaviour
{
    // Start is called before the first frame update
    CheckPoint father;
    void Start()
    {
        father = transform.parent.GetComponent<CheckPoint>();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick() { 
    CheckPointEdit.instance.Show(father);
    }
}
