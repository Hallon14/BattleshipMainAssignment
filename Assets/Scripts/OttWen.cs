using System.Collections.Generic;
using UnityEngine;

public class OttWen : IBattleship
{

    bool[,] myPlayingField;
    int[,] opposingPlayerField;

    public Vector2Int gridSize;


    //Ships
    //1x Battleship (4x long)
    //2x Cruisers (3x long)
    //1x Patrol boat (2x long)
    //1x Submarine (1x long)
    string[] Ships = { "Battleship", "Cruiser", "Cruiser", "Patrol Boat", "Submarine" };
    Dictionary<string, int> Shipdata = new Dictionary<string, int>();
    //Variables to offset ship from eachother to spread them out
    public int gridOffsetx; 
    public int gridOffsety; 


    public string GetName()
    {
        return "Admiral Otto Wendt";
    }

    public bool[,] NewGame(Vector2Int gridSize, string opponentName)
    {
        //Takes the gridsize from function and created a grid for me and my foe.
        //Note int[] for opposing side to keep track of sunken ships.
        this.gridSize = gridSize;
        myPlayingField = new bool[gridSize.x, gridSize.y];
        opposingPlayerField = new int[gridSize.x, gridSize.y];

        //Setting offset variables dynamically
        gridOffsetx = Mathf.RoundToInt(gridSize.x / 10);
        gridOffsety = Mathf.RoundToInt(gridSize.y / 10);



        //Adding data for each type of battleship to the dictionary, Cruiser only needed once since it has two entries in the list above. (string[]ships)
        Shipdata.Add("Battleship", 4);
        Shipdata.Add("Cruiser", 3);
        Shipdata.Add("Patrol Boat", 2);
        Shipdata.Add("Submarine", 1);

        //placeing all ships with helperfunction (see below)
        foreach (string ship in Ships)
        {
            placeShip(ship);
        }

        return myPlayingField;
    }

    private void placeShip(string ship)
    {
        //0 for horizontal, 1 for vertical.
        int rotation = Random.Range(0, 2);

        //Choses valid anchor point with helper funciton
        Vector2Int anchorPoint = chooseAnchorPoint(rotation, ship);
        
        //Places ship on grid based on rotation
        if (rotation == 0)
        {
            for (int i = 0; i < Shipdata[ship]; i++)
            {
                myPlayingField[anchorPoint.x + i, anchorPoint.y] = true;
            }
        }
        else
        {
            for (int i = 0; i < Shipdata[ship]; i++)
            {
                myPlayingField[anchorPoint.x, anchorPoint.y + i] = true;
            }
        }
    }

    private Vector2Int chooseAnchorPoint(int rotation, string ship)
    {
        Vector2Int anchorPoint = Vector2Int.zero;
        bool pointOkay = false;
        while (!pointOkay)
        {
            int failCount = 0;
            anchorPoint = rotation == 0 ?
                new Vector2Int(Random.Range(0, gridSize.x - Shipdata[ship]), Random.Range(0, gridSize.y)) :
                new Vector2Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y - Shipdata[ship]));

            int distanceToYMax = gridSize.y - anchorPoint.y;
            int distanceToXMax = gridSize.x - anchorPoint.x;
            int yCheckMax = Mathf.Min(distanceToYMax, gridOffsety);
            int xCheckMax = Mathf.Min(distanceToXMax, gridOffsetx);
            int yCheckMin = Mathf.Min(anchorPoint.y, gridOffsety);
            int xCheckMin = Mathf.Min(anchorPoint.x, gridOffsetx);


            for (int x = anchorPoint.x - xCheckMin; x < anchorPoint.x + xCheckMax; x++)
            {
                for (int y = anchorPoint.y - yCheckMin; y < anchorPoint.y + yCheckMax; y++)
                {
                    if (myPlayingField[x, y])
                    {
                        failCount++;
                    }
                }
            }
            if (failCount == 0){
                pointOkay = true;
            }
        }
        //Debug.Log(anchorPoint + " Chosen for " + ship);
        return anchorPoint;

    }

    public Vector2Int Fire()
    {
        throw new System.NotImplementedException();
    }

    public void Result(Vector2Int position, bool hit, bool sunk)
    {
        throw new System.NotImplementedException();
    }
} 
