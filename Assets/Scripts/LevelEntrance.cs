using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntrance : MonoBehaviour
{
    public enum Direction { Forward, Backward, None };

    public Direction up;
    public Direction down;
    public Direction right;
    public Direction left;

    public List<Vector2> NextLevelDirections;
    public bool IsWorldTransition;
    public string sceneName;
}
