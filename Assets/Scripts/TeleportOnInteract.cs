using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnInteract : MonoBehaviour
{

    [SerializeField] Transform destination;
    Vector3 destinationPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if(destination != null)
            destinationPos = destination.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            var thisCollider = GetComponent<Collider2D>();
            
            if(!thisCollider.IsTouching(PlayerCharacter.singleton.GetComponent<Collider2D>()))
                return;
            
            TransitionController.singleton.transitionIn(destinationPos);

            Invoke("transitionOut", 1.3f);
            
        }
    }

    void transitionOut()
    {
        TransitionController.singleton.transitionOut();
    }
}
