using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISnapToPixel : MonoBehaviour
{

    void LateUpdate()
    {
        Vector3 position = transform.localPosition;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        transform.localPosition = position;
    }
}


