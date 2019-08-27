using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Indicator : MonoBehaviour
{
    private Camera mainCam;
    private GameObject indicatorPrefab;
    public GameObject IndicatorLink;

    private bool initialaized;
    
    public void Init(Camera camera, GameObject prefab)
    {
        mainCam = camera;
        indicatorPrefab = prefab;
        Create();
		initialaized=true;

    }

    void Update()
    {
        if (initialaized)
        {
            UpdateIndicator();
        }
    }
    
    private void Create()
    {
        IndicatorLink = Instantiate(indicatorPrefab, GameObject.FindGameObjectWithTag("Canvas").transform);
        IndicatorLink.GetComponent<IndicatorProperty>().LinkedObject = gameObject;
        
        //IndicatorLink.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    public void Delete()
    {
        Destroy(IndicatorLink);
        initialaized=false;
    }


    private void UpdateIndicator()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(transform.position);
        if (screenPos.z > 0)
        {
            IndicatorLink.GetComponent<RectTransform>().position = screenPos.x * Vector3.right + screenPos.y * Vector3.up;
        }
    }
}