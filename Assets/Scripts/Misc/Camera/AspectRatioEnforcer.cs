using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AspectRatioEnforcer : MonoBehaviour
{
    [SerializeField] Vector2 targetAspectRatio;

    void Start()
    {
        run(targetAspectRatio.x, targetAspectRatio.y);
    }

    public void run(float x, float y)
    {
        if(x < 1  || y < 1)
        {
            Debug.LogError($"Recieved Invalid Ration {x}:{y}");
            return;
        }
            
        float targetAspect = x / y; // 4:3 ratio

        Camera cam = GetComponent<Camera>();

        // Current screen aspect ratio
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // Add letterboxing (black bars on the sides)
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            // Add pillarboxing (black bars on top/bottom)
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
