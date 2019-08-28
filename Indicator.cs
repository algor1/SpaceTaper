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
            screenPos = screenPos.x * Vector3.right + screenPos.y * Vector3.up;

        }
        else
        {
            screenPos = screenPos * -1;
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0)/2;

            float angle = Mathf.Atan2(screenPos.y, screenPos.x);
            angle -= 90 * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            screenPos = screenCenter + new Vector3(sin * 150, cos * 150, 0);
            float m = cos / sin;
            Vector3 screenBounds = screenCenter * 0.97f;

            if (cos > 0)
            {
                screenPos = new Vector3(screenBounds.y / m, screenBounds.y, 0);
            }
            else
            {
                screenPos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);
            }

            if (screenPos.x > screenBounds.x)
            {
                screenPos = new Vector3(screenBounds.x, screenBounds.x * m, 0);
            }
            else if (screenPos.x < -screenBounds.x)
            {
                screenPos = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);
            }
            screenPos += screenCenter;
        }
        IndicatorLink.GetComponent<RectTransform>().position = screenPos;



    }
}