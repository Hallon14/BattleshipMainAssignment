using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{

    /* 
    Change playerGrid and gridSize to public within Roberts code.'
    Aswell as the changes he told us to make like the name of the AI and (optionally) the amount of tests you want to run.
    Make sure you drag the tiles and tilemap to the correct slot in the inspector
     */
    public Tilemap tilemap;    
    public Tile shipTile;     
    public Tile waterTile;
    public BattleshipValidate battleshipValidate;
    public bool[,] grid;


    public void DrawGrid()
    {

        int width = battleshipValidate.gridSize.x;
        int height = battleshipValidate.gridSize.y;
        grid = battleshipValidate.playerGrid;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                Tile tileToPlace = grid[x, y] ? shipTile : waterTile;
                tilemap.SetTile(pos, tileToPlace);
            }
        }
    }
}
