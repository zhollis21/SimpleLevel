using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private const float MOVEMENT_SPEED = 40;
    private int direction;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(levels != null, "Levels on the player are null!");
    }

    // Update is called once per frame
    void Update()
    {
        LevelEntrance selectedLevel = levels[levelIndex];

        if (!IsMoving)
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

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
                levelIndex--;
                IsMoving = false;
            }
            else if (movingIndex == movingDirections.Count)
            {
                levelIndex++;
                IsMoving = false;
            }
        }
    }

    private void SetupMovement(LevelEntrance selectedLevel, LevelEntrance.Direction directionOfMovement)
    {
        if (directionOfMovement == LevelEntrance.Direction.Forward)
        {
            movingDirections = selectedLevel.forwardDirections;
            direction = 1;
            movingIndex = 0;
        }
        else
        {
            movingDirections = selectedLevel.backwardDirections;
            direction = -1;
            movingIndex = movingDirections.Count - 1;
        }
        IsMoving = true;
    }
}
