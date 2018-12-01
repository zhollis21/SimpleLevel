using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Camera mainCamera;
    public int minimumStageY;

    private const int CAMERA_RADIUS_VERTICAL = 10;

// This is called before all other start methods
private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCameraPosition(Vector2 destination)
    {

        // We don't want the camera following the player all the way to their death if they fall.
        float yValue = Mathf.Max(destination.y, minimumStageY + CAMERA_RADIUS_VERTICAL);

        mainCamera.transform.position = new Vector3(destination.x, yValue, mainCamera.transform.position.z);
    }
}
