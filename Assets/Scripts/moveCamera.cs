using UnityEngine;
using UnityEngine.Tilemaps;

public class moveCamera : MonoBehaviour
{
    public static moveCamera Instance = null;

    public Tilemap playerTilemap;
    public Tilemap aiTilemap;

    public float extraSpace = 1f;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void moveCameraToFit()
    {
        //Calculated the bounds for both the grids.
        Bounds bounds = playerTilemap.localBounds;
        bounds.Encapsulate(aiTilemap.localBounds);
        

        //Moves the camera
        transform.position = new Vector3(bounds.center.x, bounds.center.y, transform.position.z);

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = bounds.size.x / bounds.size.y;
        if (screenRatio >= targetRatio)
        {
            // Screen is wider than the tilemap
            cam.orthographicSize = bounds.size.y / 2f + extraSpace;
        }
        else
        {
            // Screen is taller than the tilemap
            float differenceInSize = targetRatio / screenRatio;
            cam.orthographicSize = (bounds.size.y / 2f * differenceInSize) + extraSpace;
        }
    }
}