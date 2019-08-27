using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private GameObject indicatorprefab;

    public float SpawnFrequency;
    [SerializeField]
    private Transform prefab;
    [SerializeField]
    private float angleRange;
    [SerializeField]
    private float massRange;
    [SerializeField]
    private float spawnDistance;
    [SerializeField]
    private float aproximateTimeToPlayer;



    private bool spawnStarted;


    private void Start()
    {
        StartSpawn();
    }
    public void  StartSpawn()
    {
          StartCoroutine(ObjecInstatiator());
    }
    public void StopSpawn()
    {
        spawnStarted = false;
    }

    private IEnumerator ObjecInstatiator()
    {
        spawnStarted = true;
        while (spawnStarted)
        {


            Vector3 randomPosition = PositionGenerator();
            float mass = MassGenerator();

            var spaceObject = Instantiate(prefab, randomPosition, new Quaternion(1, 0, 0, 0));
            var indicator=spaceObject.gameObject.AddComponent<Indicator>();
            indicator.Init( mainCam,indicatorprefab);

            spaceObject.GetComponent<Rigidbody>().mass = mass;
            spaceObject.GetComponent<Rigidbody>().AddForce(randomPosition.normalized*-1*mass*spawnDistance/aproximateTimeToPlayer);

            yield return new WaitForSeconds(SpawnFrequency);
        }

    }

    private float MassGenerator()
    {
        return UnityEngine.Random.Range(0f, massRange);
    }

    private  Vector3 PositionGenerator()
    {
        Quaternion direction = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0f, angleRange), UnityEngine.Random.Range(0f, angleRange), 0));
        return direction * Vector3.forward * spawnDistance;
    }

}

