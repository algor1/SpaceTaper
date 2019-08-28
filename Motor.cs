using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Motor : MonoBehaviour
{
    public float Health{get;set;}
    public float Hit {get;set;}

    public void Damage(float hitPoints)
    {
        Health-= hitPoints;
        if (Health<=0 ) 
        {
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Delete();
    }

    public void Delete()
    {
        EventHandler<DestroyedEventArgs> handler = Destroed;
        handler?.Invoke(this, new DestroyedEventArgs {HitPoints= Hit});
        GetComponent<Indicator>().Delete();
        Destroy(gameObject);
    }

    public event EventHandler<DestroyedEventArgs> Destroed;
}
public class DestroyedEventArgs : EventArgs
{
    public float HitPoints{get;set;}
}
