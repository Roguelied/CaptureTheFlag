using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TillePainter : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile blackSquer;
    public Vector3Int position;

    [ContextMenu("Paint")]
    void Paint()
    {
         tilemap.AddTileFlags(position,TileFlags.None);
        tilemap.SetColor(position,Color.black);
    }
}
