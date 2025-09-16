using System.Collections.Generic;
using UnityEngine;

public enum rotation
{
    horizontal,
    vertical,
}

public enum shipType
{
    Battleship,
    Cruiser,
    Patrol_Boat,
    Submarine
}

public class OttWen : IBattleship
{

    bool[,] myBoard;
    public Vector2Int fieldSize;


    //Ships
    //1x Battleship (4x long)
    //2x Cruisers (3x long)
    //1x Patrol boat (2x long)
    //1x Submarine (1x long)
    shipType[] myShips = { shipType.Battleship, shipType.Cruiser, shipType.Cruiser, shipType.Patrol_Boat, shipType.Submarine };
    Dictionary<shipType, int> shipData = new Dictionary<shipType, int>();

    //Variables to offset ship from eachother to spread them out
    int xSpace;
    int ySpace;

    //Function to return my AI player name to Robert-Sensei
    public string GetName()
    {
        return "Distinguished Admiral Erik Otto Wendt XIV, Commander of the Northern Fleets";
    }

    public bool[,] NewGame(Vector2Int gridSize, string opponentName)
    {
        //Takes the gridsize from function and created a grid for me and my foe.
        //Note int[] for opposing side to keep track of sunken ships.
        fieldSize = gridSize;
        myBoard = new bool[fieldSize.x, fieldSize.y];
        xSpace = 2;
        ySpace = 2;


        //Adding data for each type of battleship to the dictionary, Cruiser only needed once since it has two entries in the list above. (string[]ships)
        shipData.Add(shipType.Battleship, 4);
        shipData.Add(shipType.Cruiser, 3);
        shipData.Add(shipType.Patrol_Boat, 2);
        shipData.Add(shipType.Submarine, 1);

        //placeing all ships with helperfunction (see below)
        foreach (shipType vessel in myShips)
        {
            placeShip(vessel);
        }
        return myBoard;
    }

    // Helper function to place the ship on designated spot. Different for loops depending on rotation. If rotation not found an exeption is thrown
    private void placeShip(shipType vessel)
    {
        //Selecting rotaion of ship
        int choseRotation = 0; //Random.Range(0, 1);
        rotation shipRotation = choseRotation == 0 ? rotation.horizontal : rotation.vertical;

        //Choseing a point and validates it with helperfunction below
        Vector2Int anchorPoint = Vector2Int.zero;
        int attempts = 0;
        bool pointOkay = false;

        while (!pointOkay || attempts <= 1000)
        {
            //Selecting an anchor point randomly with modified x and y values based on rotaion.
            anchorPoint = shipRotation == rotation.horizontal ?
                new Vector2Int(Random.Range(0, fieldSize.x - shipData[vessel] - 1), Random.Range(0, fieldSize.y)) :
                new Vector2Int(Random.Range(0, fieldSize.x), Random.Range(0, fieldSize.y - shipData[vessel] - 1));

            //Validates anchor point, if it isnt posible, throw exception
            pointOkay = validatePoint(anchorPoint, shipRotation, vessel);
            attempts++;

            if (attempts > 1000)
            {
                throw new System.Exception("Couldnt find valid anchor point within 1000 attempts");
            }
        }

        //Placeing ships based on rotation.
        if (shipRotation == rotation.horizontal)
        {
            for (int i = 0; i < shipData[vessel]; i++)
            {
                myBoard[anchorPoint.x + i, anchorPoint.y] = true;
            }
        }

        else if (shipRotation == rotation.vertical)
        {
            for (int i = 0; i < shipData[vessel]; i++)
            {
                myBoard[anchorPoint.x, anchorPoint.y + i] = true;
            }
        }
        else
        {
            throw new System.Exception("Rotation of ship not found");
        }
    }

    private bool validatePoint(Vector2Int point, rotation rotation, shipType vessel)
    {
        //First Check, checks if the point itself is a valid starting point.
        if (myBoard[point.x, point.y])
        {
            return false;
        }

        //Second Check, checks the position of the entire ship
        if (rotation == rotation.horizontal)
        {
            for (int i = 0; i < shipData[vessel]; i++)
            {
                if (myBoard[point.x + i, point.y])
                {
                    return false;
                }
            }
        }
        if (rotation == rotation.vertical)
        {
            for (int i = 0; i < shipData[vessel]; i++)
            {
                if (myBoard[point.x, point.y + i])
                {
                    return false;
                }
            }
        }
        //Third Check, looks at the surrounding tiles aswell.
        //Declaring start and end variables for the grid search
        /*         int xFrom = point.x - Mathf.Min(xSpace, point.y);
                int xTo = point.x + shipData[vessel] + Mathf.Min(xSpace, fieldSize.x - point.x);
                int yFrom = point.y - Mathf.Min(ySpace, point.x);
                int yTo = point.y + Mathf.Min(ySpace, fieldSize.y - point.y); */

        int xFrom = xSpace >= point.y ?
            point.x - point.y :
            point.x - xSpace;

        int xTo = fieldSize.x - point.x >= xSpace ?
            point.x + xSpace :
            point.x + (fieldSize.x - point.x);

        int yFrom = ySpace >= point.x ?
            point.y - point.x :
            point.y - ySpace;

        int yTo = fieldSize.y - point.y >= ySpace ?
            point.y + ySpace :
            point.y + (fieldSize.x - point.x);

        for (int x = xFrom; x < xTo; x++)
        {
            for (int y = yFrom; y < yTo; y++)
            {
                if (myBoard[x, y])
                {
                    return false;
                }
            }
        }

        //All tests passed, return true
        return true;
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
