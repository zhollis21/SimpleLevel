using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save {

    public List<int> uncollectedCoinsPositions = new List<int>();
    
    public int lives = 0;
    public int money = 0;
    public int level = 0;

}
