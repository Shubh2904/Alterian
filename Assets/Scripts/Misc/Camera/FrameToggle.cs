using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FrameToggle : MonoBehaviour
{
    [SerializeField] PixelPerfectCamera pixelPerfectCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool toggled = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            toggled = !toggled;

            if(toggled)
            {
                pixelPerfectCam.cropFrame = PixelPerfectCamera.CropFrame.StretchFill;
            }
            else 
            {
                pixelPerfectCam.cropFrame = PixelPerfectCamera.CropFrame.Windowbox;
            }
        }
    }
}
