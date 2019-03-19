using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionPlayer : MonoBehaviour
{
    public string sceneNameOfNextOverworld;
    public string sceneNameOfPreviousOverworld;
    public List<LevelEntrance> levels;

    private const float BUTTON_FORCE = .1f;
    private int levelIndex;
    private bool IsMoving;
    private List<Vector2> movingDirections;
    private int movingIndex;
    private const float MOVEMENT_SPEED = 60;
    private int direction;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(levels != null, "Levels on the player are null!");
    }

    // Update is called once per frame
    void Update()
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
            return;

        LevelEntrance selectedLevel = levels[levelIndex];

        if (!IsMoving)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            float spacebarInput = Input.GetAxis("Jump");

            if (spacebarInput > .5 && !string.IsNullOrEmpty(selectedLevel.sceneName))
                SceneManager.LoadScene(selectedLevel.sceneName);

            if (verticalInput > BUTTON_FORCE && selectedLevel.up != LevelEntrance.Direction.None)
            {
                SetupMovement(selectedLevel, selectedLevel.up);
            }
            else if (verticalInput < -BUTTON_FORCE && selectedLevel.down != LevelEntrance.Direction.None)
            {
                SetupMovement(selectedLevel, selectedLevel.down);
            }
            else if (horizontalInput > BUTTON_FORCE && selectedLevel.right != LevelEntrance.Direction.None)
            {
                SetupMovement(selectedLevel, selectedLevel.right);
            }
            else if (horizontalInput < -BUTTON_FORCE && selectedLevel.left != LevelEntrance.Direction.None)
            {
                SetupMovement(selectedLevel, selectedLevel.left);
            }
        }
        else
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, movingDirections[movingIndex], MOVEMENT_SPEED * Time.deltaTime);

            if (Vector2.Distance(transform.localPosition, movingDirections[movingIndex]) < .1)
                movingIndex += direction;

            if (movingIndex == -1)
            {
                IsMoving = false;

                if (selectedLevel.IsWorldTransition)
                    SceneManager.LoadScene(selectedLevel.sceneName);
            }
            else if (movingIndex == movingDirections.Count)
            {
                levelIndex++;
                IsMoving = false;

                if (levels[levelIndex].IsWorldTransition)
                    SceneManager.LoadScene(levels[levelIndex].sceneName);
            }
        }
    }

    private void SetupMovement(LevelEntrance selectedLevel, LevelEntrance.Direction directionOfMovement)
    {
        if (directionOfMovement == LevelEntrance.Direction.Forward && selectedLevel != null && selectedLevel.NextLevelDirections.Count > 0)
        {
            movingDirections = selectedLevel.NextLevelDirections;
            direction = 1;
            movingIndex = 0;

            IsMoving = true;
        }
        else if (directionOfMovement == LevelEntrance.Direction.Backward && levelIndex > 0 && levelIndex <= levels.Count && levels[levelIndex - 1].NextLevelDirections.Count > 0)
        {
            levelIndex--;
            selectedLevel = levels[levelIndex];
            movingDirections = selectedLevel.NextLevelDirections;
            direction = -1;
            movingIndex = movingDirections.Count - 1;

            IsMoving = true;
        }
    }
}
