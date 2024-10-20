using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{

    [Tooltip("Material that has shader Circular Sprite Inverted Shader")]
    [SerializeField] Material transitionMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transitionIn()
    {
        //set curtain postion to player position
        
        //lerp transiton from 1 to 0 for curtain radius

    }

    public void transitionOut()
    {
        //set curtain postion to player position
        //lerp transiton from 0 to 1 for curtain radius

    }

    void setCurtainRadius(float radius)
    {
        transitionMaterial.SetFloat("Mask Radius", radius);
    }
    
}
