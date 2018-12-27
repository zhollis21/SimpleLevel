using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenArea : MonoBehaviour
{

    private TilemapRenderer mapRenderer;

    // Use this for initialization
    void Start()
    {
        mapRenderer = GetComponent<TilemapRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mapRenderer != null)
        {
            mapRenderer.sortingLayerName = "Default";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (mapRenderer != null)
        {
            mapRenderer.sortingLayerName = "Hidden Area";
        }
    }
}
