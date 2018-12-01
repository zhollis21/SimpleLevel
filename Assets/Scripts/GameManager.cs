using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Game Management is normally handled singleton style
    public static GameManager instance;

    public Camera mainCamera;
    public int minimumStageY;
    public Text scoreText;
    public Transform playerTransform;

    private const int CAMERA_RADIUS_VERTICAL = 10;
    private Vector2 playerSpawnPoint;
    private int score;

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
        SetCameraPosition();
    }

    // LateUpdate is called once per frame, after all Update functions
    void LateUpdate()
    {
        CheckForOutOfBoundsOrDeath();
        SetCameraPosition();
    }

    public void SetPlayerSpawnPoint(Vector2 point)
    {
        playerSpawnPoint = point;
    }

    public void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    private void CheckForOutOfBoundsOrDeath()
    {
        // Save the player if they fall off
        if (playerTransform.position.y < minimumStageY - 5)
        {
            playerTransform.position = playerSpawnPoint;

            // Take away a point for saving them
            if (score > 0)
            {
                score--;
                scoreText.text = "Score: " + score;
            }
        }
    }

    private void SetCameraPosition()
    {
        // We don't want the camera following the player all the way to their death if they fall.
        float yValue = Mathf.Max(playerTransform.position.y, minimumStageY + CAMERA_RADIUS_VERTICAL);

        mainCamera.transform.position = new Vector3(playerTransform.position.x, yValue, mainCamera.transform.position.z);
    }
}
