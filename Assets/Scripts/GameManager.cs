using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{

    // Game Management is normally handled singleton style
    public static GameManager instance;

    public Camera mainCamera;
    public int stageBottom;
    public int stageLeft;
    public int stageRight;
    public Text scoreText;
    public Transform playerTransform;
    public Vector2 playerSpawnPoint;

    private const int CAMERA_RADIUS_VERTICAL = 10;
    private const int CAMERA_RADIUS_HORIZONTAL = 18;
    private const int CAMERA_GRACE_HORIZONTAL = 5;
    private const int CAMERA_GRACE_VERTICAL = 1;
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
        CheckForOutOfBounds();
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

    public void RevivePlayer()
    {
        playerTransform.position = playerSpawnPoint;

        // Take away a point for saving them
        if (score > 0)
        {
            score--;
            scoreText.text = "Score: " + score;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CheckForOutOfBounds()
    {
        // Reset the player if they go out of bounds
        if (playerTransform.position.y < stageBottom - CAMERA_RADIUS_VERTICAL / 2 ||
            playerTransform.position.x < stageLeft - CAMERA_RADIUS_HORIZONTAL / 4 ||
            playerTransform.position.x > stageRight + CAMERA_RADIUS_HORIZONTAL / 4)
        {
            RevivePlayer();
        }
    }

    private void SetCameraPosition()
    {
        float xPosition = mainCamera.transform.position.x;
        float yPosition = mainCamera.transform.position.y;

        // Set the xPosition of the Camera
        // We don't want the camera following the player off the map if they fall.
        if (playerTransform.position.y < stageBottom + CAMERA_RADIUS_VERTICAL - CAMERA_GRACE_VERTICAL)
            yPosition = stageBottom + CAMERA_RADIUS_VERTICAL;

        // Move the camera down
        else if (playerTransform.position.y < mainCamera.transform.position.y - CAMERA_GRACE_VERTICAL)
            yPosition = playerTransform.position.y + CAMERA_GRACE_VERTICAL;

        // Move the camera up
        else if (playerTransform.position.y > mainCamera.transform.position.y + CAMERA_GRACE_VERTICAL)
            yPosition = playerTransform.position.y - CAMERA_GRACE_VERTICAL;

        // Set the yPosition of the Camera
        // We don't want the camera following the player off the map if they jump far to the left
        if (playerTransform.position.x < stageLeft + CAMERA_RADIUS_HORIZONTAL - CAMERA_GRACE_HORIZONTAL)
            xPosition = stageLeft + CAMERA_RADIUS_HORIZONTAL;

        // We don't want the camera following the player off the map if they jump far to the right
        else if (playerTransform.position.x > stageRight - CAMERA_RADIUS_HORIZONTAL + CAMERA_GRACE_HORIZONTAL)
            xPosition = stageRight - CAMERA_RADIUS_HORIZONTAL;

        // Move the camera left
        else if (playerTransform.position.x < mainCamera.transform.position.x - CAMERA_GRACE_HORIZONTAL)
            xPosition = playerTransform.position.x + CAMERA_GRACE_HORIZONTAL;

        // Move the camera right
        else if (playerTransform.position.x > mainCamera.transform.position.x + CAMERA_GRACE_HORIZONTAL)
            xPosition = playerTransform.position.x - CAMERA_GRACE_HORIZONTAL;

        mainCamera.transform.position = new Vector3(xPosition, yPosition, mainCamera.transform.position.z);
    }

    /// <summary>
    /// Takes the current state of the game and puts it into a save object. 
    /// </summary>
    /// <returns>A Save Object filled with current data.</returns>
    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.money = score;

        //intentionally not saving the player position so that the player cant save scum

        //ToDo: Save which coins are collected so they cant be re-collected
        //ToDo: Save how many levels the player has completed
        //ToDo: Save how many lives the player has remaining?

        return save;
    }

    public void SaveGame()
    {
        //Create the default save game 
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    //ToDo: Implement Way to call the Save Game
    //ToDo: Implement Load Game

}
