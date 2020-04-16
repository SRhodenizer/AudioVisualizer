using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateCubes : MonoBehaviour
{
    public GameObject cubePrefab;
    public float positioningDistance;
    public float maxScale;
    private GameObject[] sampleCubeArr = new GameObject[512];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < sampleCubeArr.Length; i++)
        {
            GameObject sampleCubeInstance = (GameObject)Instantiate(cubePrefab);
            sampleCubeInstance.transform.position = this.transform.position;
            sampleCubeInstance.transform.parent = this.transform;
            sampleCubeInstance.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            sampleCubeInstance.transform.position = Vector3.forward * positioningDistance;
            sampleCubeArr[i] = sampleCubeInstance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < sampleCubeArr.Length; i++)
        {
            if(sampleCubeArr != null)
            {
                sampleCubeArr[i].transform.localScale = new Vector3(1, AudioPeer.samples[i] * maxScale + 2, 1);
            }
        }
    }
}
