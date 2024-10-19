using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISnapToPixel : MonoBehaviour
{
    //fixes for UI jiterring when positioned at floating number while camera moves

    void LateUpdate()
    {
        Vector3 position = transform.localPosition;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        transform.localPosition = position;
    }
}


