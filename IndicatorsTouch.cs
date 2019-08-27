using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IndicatorsTouch : MonoBehaviour
{
    void Update()
    {
       

            TouchEvents();
     
    }


    void TouchEvents()
    {
        GraphicRaycaster graphicRaycaster = this.GetComponent<GraphicRaycaster>();
        PointerEventData pointerEventData = new PointerEventData(null);

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                pointerEventData.position = Input.GetTouch(i).position;
                //Create list to receive all results
                List<RaycastResult> results = new List<RaycastResult>();
                //Raycast it
                graphicRaycaster.Raycast(pointerEventData, results);

                for( int j=0 ; j<results.Count ; j++ )
                {
                    if (results[i].gameObject.TryGetComponent(typeof(IndicatorProperty), out Component c))
                    {
                        var indicatorProperty = (IndicatorProperty)c;
                        indicatorProperty.LinkedObject.GetComponent<Motor>().Damage(1);
                            break;
                    }
                }
            }
        }

    }

}