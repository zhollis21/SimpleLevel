using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenArea : MonoBehaviour
{

    private Tilemap tilemap;

    // Use this for initialization
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        Debug.Assert(tilemap != null, "Hidden Area's Tilemap is null.");
    }

    // Entering the hidden area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tilemap != null)
        {
            // Sets the color's alpha to 0 (makes it transparent)
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 0);
        }
    }

    // Leaving the Hidden area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (tilemap != null)
        {
            // Sets the color's alpha to 255 (makes it fully visible)
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 255);
        }
    }
}
