using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


[Serializable]
struct ResolutionData
{
    public Vector2Int size;
    public Sprite sizeIndicator;
};

[RequireComponent(typeof(PixelPerfectCamera))]
public class ResolutionToggle : MonoBehaviour
{

    [SerializeField] SpriteRenderer resolutionIndicator;
    PixelPerfectCamera pixelPerfectCam;

    [SerializeField] List<ResolutionData> resolutions;



    // Start is called before the first frame update
    void Start()
    {
        pixelPerfectCam = GetComponent<PixelPerfectCamera>();
    }

    int counter = 0;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            counter += 1;

            int index = counter % resolutions.Count;

            pixelPerfectCam.refResolutionX = resolutions[index].size.x; 
            pixelPerfectCam.refResolutionY = resolutions[index].size.y;

            resolutionIndicator.sprite = resolutions[index].sizeIndicator;
            
        }
        
    }
}
