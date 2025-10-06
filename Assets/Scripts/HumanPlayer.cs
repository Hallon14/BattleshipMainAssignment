using UnityEngine;

public class HumanPlayer
{
    public bool[,] playerField;

    public Vector2Int Fire()
    {
        return Vector2Int.zero;
    }

    public bool[,] NewGame(int xSize, int ySize)
    {
        playerField = new bool[xSize, ySize];
        return playerField;
    }
}
