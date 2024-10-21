using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TransitionController : MonoBehaviour
{

    
    Material transitionMaterial;
    [Header("Placed on gameobject having material with shader TransparentCircleMask")]
    [SerializeField] float transitionDuration = 0.4f;

    public static TransitionController singleton;

    Vector3 teleportPos;


    void Awake()
    {
        transitionMaterial = GetComponent<SpriteRenderer>().material;

        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void transitionIn(Action onFinish=null)
    {
        //set curtain postion to player position
        this.transform.position = PlayerCharacter.singleton.transform.position;
        //lerp transiton from 1 to 0 for curtain radius
        LeanTween.value(this.gameObject, setCurtainRadius, 1, 0, transitionDuration).setOnComplete(onFinish);
    }

    public void transitionIn(Vector3 transistionPos)
    {
        teleportPos = transistionPos;

        transitionIn(teleportPlayer);
    }



    public void transitionOut(Action onFinish=null)
    {
        var playerPos = PlayerCharacter.singleton.transform.position;

        Camera.main.transform.position = new Vector3(playerPos.x, playerPos.y, Camera.main.transform.position.z);

        //set curtain postion to player position
        this.transform.position = PlayerCharacter.singleton.transform.position;

        //lerp transiton from 0 to 1 for curtain radius
        LeanTween.value(this.gameObject, setCurtainRadius, 0, 1, transitionDuration).setOnComplete(onFinish);

    }


    void setCurtainRadius(float radius)
    {
        transitionMaterial.SetFloat("_MaskRadius", radius);
    }

    void teleportPlayer()
    {
        PlayerCharacter.singleton.transform.position = teleportPos;
    }
    
}
