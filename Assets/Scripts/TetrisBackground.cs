using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tetronimos = new GameObject[7];

    [SerializeField]
    private GameObject[] spheres = new GameObject[2];

    private Material mat;
    private int timerCount = 0;

    public float startScale, scaleMultiplier;
    public int band;

    public float posY = 20;

    // Start is called before the first frame update
    void Start()
    {
        // mat = GetComponent<MeshRenderer>().materials[0];
        // sphereRight = GameObject.Find("SphereRight");
        // sphereLeft = GameObject.Find("SphereLeft");
    }


    // make changes here
    // Update is called once per frame
    void Update()
    {
        // transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBandBuffer[band] * scaleMultiplier) + startScale, transform.localScale.z);
        // Color color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
        // mat.SetColor("_EmissionColor", color);

        //Debug.Log("Amp: " + (Mathf.Floor(AudioPeer.amplitude * 300)));
        // int temp = (Mathf.RoundToInt(AudioPeer.amplitude / 10));
        if ((timerCount % (Mathf.Floor(AudioPeer.amplitude * 300))) == 0)
        {
            // spawn shit new Vector3(Random.Range(-90,93), 10, 10.86f)
            GameObject block = Instantiate(tetronimos[Random.Range(0, 7)], new Vector3(Random.Range(-90, 93), posY, 10.86f), Quaternion.identity);
            block.AddComponent<Rigidbody>();
            block.AddComponent<Destroyer>();
            block.transform.localScale = block.transform.localScale * scaleMultiplier;
            block.tag = "tetronimo";
            // block.transform.localScale = new Vector3(transform.localScale.x * scaleMultiplier + startScale, (AudioPeer.audioBandBuffer[band] * scaleMultiplier) + startScale, transform.localScale.z * scaleMultiplier + startScale);

        }

        if((timerCount % (Mathf.Floor(AudioPeer.amplitude * 500))) == 0)
        {
            for(int i = 0; i < spheres.Length; i++)
            {
                for (int j = 0; j < tetronimos.Length; j++)
                {
                    GameObject block = Instantiate(tetronimos[j], spheres[i].transform.position, Quaternion.identity);
                    block.AddComponent<Rigidbody>();
                    block.AddComponent<Destroyer>();
                    block.transform.localScale = block.transform.localScale * scaleMultiplier;
                    block.tag = "tetronimo";
                }
            }
            
        }

        timerCount++;
    }
}
