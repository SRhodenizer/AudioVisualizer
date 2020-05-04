using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tetronimos = new GameObject[7];

    [SerializeField]
    private GameObject[] tetroSphere = new GameObject[27];

    private Material mat;
    private int timerCount = 0;
    
    public float startScale, scaleMultiplier;
    public int band;

    public float posY = 20;

    // Start is called before the first frame update
    void Start()
    {

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
        timerCount++;
    }

    public void Active()
    {
        for(int i = 0; i < tetroSphere.Length; i++)
        {
            tetroSphere[i].SetActive(true);
        }
    }

    public void Inactive()
    {
        for (int i = 0; i < tetroSphere.Length; i++)
        {
            tetroSphere[i].SetActive(false);
        }
    }
}
