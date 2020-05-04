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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
