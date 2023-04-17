using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // Platform type and maximum number to spawn in level
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private int _maxPlatforms;
    [SerializeField] private GameObject _platformOutline;

    private List<GameObject> _platforms; 

    void Start()
    {
        _platforms = ObjectPooler.CreateObjectPool(_maxPlatforms, _platformPrefab);
        ObjectPooler.AssignParentGroup(_platforms, this.transform);
        _platformOutline.SetActive(true);
    }

    void Update()
    {
        _platformOutline.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1f);

        // Place platform on left click
        if (Input.GetMouseButtonDown(0))
        {
            GameObject platform = ObjectPooler.GetPooledObject(_platforms);

            // Check that there's platforms left in the pool before placing
            if (platform != null)
            {
                // Grab mouse pos and add 1 to z so platform is behind clipping plane
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z += 1;

                // Spawn platform
                platform.transform.position = mousePos;
                platform.SetActive(true);
            }
        }

        // Delete platform on right click
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   
            RaycastHit hitInfo;

            // if mouse clicks on a platform
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Platform"))
                {
                    hitInfo.collider.gameObject.SetActive(false);
                }
            }
        }

        
    }

}
