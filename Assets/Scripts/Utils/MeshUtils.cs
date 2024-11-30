using System.Collections;
using UnityEngine;
namespace Assets.Scripts.Utils
{
    public class MeshUtils : MonoBehaviour
    {
        public static GameObject GetObjectAtCoordinate()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("∂‘œÛ : " + hit.transform.name);
                return hit.transform.gameObject;
            }
            else
            {
                return null;
            }
        }
        public static Vector3 MouseMeshPosition(Transform transform)
        {
            GameObject gameObject = GetObjectAtCoordinate();
            if (gameObject != null)
            {
                return gameObject.transform.position;
            }
            return new Vector3(0, 0, 0);
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                GetObjectAtCoordinate();
            }
        }
    }
}