using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHunterBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject titleImage;

    [SerializeField]
    private Light spotLight;

    [SerializeField]
    private GameObject scoutFly;

    [SerializeField]
    private GameObject[] monsters = new GameObject[2];

    private float[] spawnPoints = new float[] { -93.0f, 90.0f };

    private int timerCount = 0;

    public float posY = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((timerCount % (Mathf.Floor(AudioPeer.amplitude * 300))) == 0)
        {
            SpawnScoutFlies();
        }

        /*
        if ((timerCount % (Mathf.Floor(AudioPeer.amplitude * 10))) == 0)
        {
            SpawnMonsters(1);
        }

        if (GameObject.FindGameObjectWithTag("monster"))
        {
            FlingMonster();
        }
        */
    }

    public void Active()
    {
        titleImage.SetActive(true);
        spotLight.enabled = true;
    }

    public void Inactive()
    {
        titleImage.SetActive(false);
        spotLight.enabled = false;
    }

    void SpawnScoutFlies()
    {
        // GameObject fly = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject fly = Instantiate(scoutFly, new Vector3(Random.Range(-90, 93), posY, 10.86f), Quaternion.identity);
        // fly.transform.position = new Vector3(Random.Range(-90, 93), posY, 10.86f);
        fly.AddComponent<Rigidbody>();
        fly.AddComponent<Destroyer>();
        fly.tag = "scoutFly";
        fly.transform.localScale = fly.transform.localScale * 2.0f;
    }

    void FlingMonster()
    {
        GameObject[] monArray = GameObject.FindGameObjectsWithTag("monster");

        for(int i = 0; i < monArray.Length; i++)
        {
            if(monArray[i].transform.position.x < 89)
            {
                monArray[i].transform.position = new Vector3(monArray[i].transform.position.x + 1.5f, monArray[i].transform.position.y, monArray[i].transform.position.z);
            }
            else //greater than
            {
                monArray[i].transform.position = new Vector3(monArray[i].transform.position.x - 1.5f, monArray[i].transform.position.y, monArray[i].transform.position.z);
            }
        }
    }

    void SpawnMonsters(int iterations)
    {
        for(int i = 0; i < iterations; i++)
        {
            GameObject monster = Instantiate(monsters[Random.Range(0, 2)], new Vector3(-90, 20, 9.86f), Quaternion.identity);
            monster.AddComponent<Destroyer>();
            monster.tag = "monster";
        }
    }
}
