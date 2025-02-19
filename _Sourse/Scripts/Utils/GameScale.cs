using UnityEngine;

public class GameScale : MonoBehaviour
{
    public float defaultFOV = 60f;
    public float baseScreenWidth = 1920f;
    public float baseScreenHeight = 1080f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();

        if (mainCamera != null && !mainCamera.orthographic)
        {
            AdjustFOV();
        }
    }

    private void AdjustFOV()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = baseScreenWidth / baseScreenHeight;

        if (screenRatio > targetRatio)
        {
            mainCamera.fieldOfView = defaultFOV * (screenRatio / targetRatio);
        }
        else
        {
            mainCamera.fieldOfView = defaultFOV;
        }
    }
}