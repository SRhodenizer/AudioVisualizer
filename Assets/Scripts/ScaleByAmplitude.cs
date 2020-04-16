using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleByAmplitude : MonoBehaviour
{

    private Material mat;
    public int band;
    public float startScale, maxScale;
    public bool useBuffer;

    public float red, green, blue;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (useBuffer)
        {
            transform.localScale = new Vector3((AudioPeer.amplitude * maxScale)+startScale, (AudioPeer.amplitude * maxScale) + startScale, (AudioPeer.amplitude * maxScale) + startScale);

            Color color = new Color(red * AudioPeer.amplitude, green*AudioPeer.amplitude, blue*AudioPeer.amplitude);

            mat.SetColor("_EmissionColor", color);
        }

        if (!useBuffer)
        {
            transform.localScale = new Vector3((AudioPeer.amplitudeBuffer * maxScale) + startScale, (AudioPeer.amplitudeBuffer * maxScale) + startScale, (AudioPeer.amplitudeBuffer * maxScale) + startScale);

            Color color = new Color(red * AudioPeer.amplitudeBuffer, green * AudioPeer.amplitudeBuffer, blue * AudioPeer.amplitudeBuffer);

            mat.SetColor("_EmissionColor", color);
        }
    }
}
