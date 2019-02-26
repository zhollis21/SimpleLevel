using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a movable and expandible platform
/// </summary>
public class Platform : MonoBehaviour
{

    public GameObject leftSprite;
    public GameObject middleSprite;
    public GameObject rightSprite;

    [Range(2, 100)]
    public int platformWidth;
    public float movementSpeed;

    public List<Vector2> patrolPoints;

    [Tooltip("When this is turned on the platform will reverse back through the patrol points after reaching the last point.")]
    public bool Trace;

    [Tooltip("When this is turned on the platform will pause after reaching each.")]
    public bool PauseAtPoints = true;

    public float PauseTime = 0.5f;

    private int patrolPointIndex = 0;
    private int playerCollidersInContact = 0;
    private int direction = 1;
    private double timeSinceReachingPatrolPoint;
    private BoxCollider2D platformCollider;

    // Use this for initialization
    void Start()
    {
        SetPlatformSpritesPositions();
        platformCollider = GetComponent<BoxCollider2D>();
        if (platformCollider != null)
            platformCollider.size = new Vector2(platformWidth, platformCollider.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoints.Count > 0)
            Patrol();
    }

    private void SetPlatformSpritesPositions()
    {
        float leftPoint = -(platformWidth - 1) / 2.0f;

        // Setting the left sprite's position
        leftSprite.transform.localPosition = new Vector2(leftPoint, 0);

        // Setting the positions of all the middle sprites
        for (int distanceFromLeftPoint = 1; distanceFromLeftPoint < platformWidth - 1; distanceFromLeftPoint++)
        {
            if (distanceFromLeftPoint == 1) // Set the original middle sprite's position
                middleSprite.transform.localPosition = new Vector2(leftPoint + distanceFromLeftPoint, 0);
            else // Copy the middle sprite and set its position
            {
                var copyOfMiddleSprite = Instantiate(middleSprite, transform);
                copyOfMiddleSprite.transform.localPosition = new Vector2(leftPoint + distanceFromLeftPoint, 0);
            }
        }

        // If the platform width is 2 then delete the middle piece
        if (platformWidth < 3)
            middleSprite.SetActive(false);

        // Setting the right sprite's position
        rightSprite.transform.localPosition = new Vector2(-leftPoint, 0);
    }

    // Moves the player toward the destination based on their movement speed
    protected void MoveTowards(Vector2 destination)
    {

        transform.position = Vector2.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
    }

    private void Patrol()
    {
        MoveTowards(patrolPoints[patrolPointIndex]);

        // If we are at the destination, lets set the next destination
        if (Mathf.Abs(patrolPoints[patrolPointIndex].x - transform.position.x) < .1 && Mathf.Abs(patrolPoints[patrolPointIndex].y - transform.position.y) < .1)
        {
            timeSinceReachingPatrolPoint += Time.deltaTime;

            if (timeSinceReachingPatrolPoint >= .5)
            {
                timeSinceReachingPatrolPoint = 0;
                patrolPointIndex += direction;

                // If we just arrived at the last point, start over
                if (patrolPointIndex == patrolPoints.Count)
                {
                    if (Trace)
                    {
                        direction = -1;
                        patrolPointIndex += direction;
                    }
                    else
                        patrolPointIndex = 0;
                }
                else if (patrolPointIndex == -1)
                {
                    direction = 1;
                    patrolPointIndex += direction;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
            collision.collider.transform.SetParent(transform);
        else if (collision.collider.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
            playerCollidersInContact++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
            collision.collider.transform.SetParent(null);
        else if (collision.collider.tag == "Player")
        {
            playerCollidersInContact--;
            // Because we have multiple colliders on the player, we need to make sure all are not touching
            if (playerCollidersInContact == 0)
                collision.collider.transform.SetParent(null);
        }
    }
}
