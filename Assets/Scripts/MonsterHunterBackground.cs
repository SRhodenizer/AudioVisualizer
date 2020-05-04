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
    }
}
