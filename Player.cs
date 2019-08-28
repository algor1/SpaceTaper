using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Armor { get; set; } = 1000;
    public float Shield { get; set; } = 1000;
    public float Power { get; set; } = 1000;
    public float ArmorMax { get; set; } = 1000;
    public float ShieldMax { get; set; } = 1000;
    public float PowerMax { get; set; } = 1000;
    public float ArmorRestoreOnTick { get; set; } = 10;
    public float ShieldRestoreOnTick { get; set; } = 10;
    public float PowerRestoreOnTick { get; set; } = 10;

    private void Start()
    {
        StartCoroutine(Tick());
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        Damage(collision.gameObject.GetComponent<Rigidbody>().mass);
    }

    private void Damage(float mass)
    {
        if (Shield-mass>=0)
        {
            Shield -= mass;
        }else if (Shield + Armor - mass >= 0)
        {
            Shield = 0;
            Armor -= mass - Shield;
        }
        else
        {
            Gameover();
        }
        print(Armor.ToString()+ Shield);
    }

    private void Gameover()
    {
        throw new NotImplementedException();
    }

    private IEnumerator Tick()
    {
        //RestorePower();
        RestoreShield();
        yield return new WaitForSeconds(1);

    }

    private void RestoreShield()
    {
        Shield += ShieldRestoreOnTick;
        if (Shield > ShieldMax)
            Shield = ShieldMax;
    }

    private void RestorePower()
    {
        throw new NotImplementedException();
    }
}
