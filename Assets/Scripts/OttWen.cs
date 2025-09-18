using System.Collections.Generic;
using NUnit.Framework.Interfaces;
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
    //Variables needed for defence
    bool[,] myBoard;
    public Vector2Int fieldSize;

    //Ships - Ships added to the dictionary during the NewGame() funciton
    shipType[] myShips = { shipType.Battleship, shipType.Cruiser, shipType.Cruiser, shipType.Patrol_Boat, shipType.Submarine };
    Dictionary<shipType, int> shipData = new Dictionary<shipType, int>();
    
    //Variables to offset ship from eachother to spread them out
    int xSpace;
    int ySpace;


    //Variables needed for offence
    private StateMachine stateMachine;
    int[,] heatMap;
    List<shipType> enemyShips = new List<shipType> { shipType.Battleship, shipType.Cruiser, shipType.Cruiser, shipType.Patrol_Boat, shipType.Submarine };
    public Vector2Int lastShot;
    public bool lastShotHit;
    public bool lastShotSunk;


    //Function to return my AI player name to Robert-Sensei
    public string GetName()
    {
        return "Distinguished Admiral Erik Otto Wendt XIV, Commander of the Northern Fleets";
    }

    public bool[,] NewGame(Vector2Int gridSize, string opponentName)
    {
        #region Start
        //Takes the gridsize from function and created a grid for me and my foe.
        //Note int[] for opposing side to keep track of probablilites
        fieldSize = gridSize;
        myBoard = new bool[fieldSize.x, fieldSize.y];
        xSpace = fieldSize.x / 7;
        ySpace = fieldSize.y / 7;
        heatMap = new int[fieldSize.x, fieldSize.y];

        //Adding data for each type of battleship to the dictionary, Cruiser only needed once since it has two entries in the list above. (string[]ships)
        shipData.Add(shipType.Battleship, 4);
        shipData.Add(shipType.Cruiser, 3);
        shipData.Add(shipType.Patrol_Boat, 2);
        shipData.Add(shipType.Submarine, 1);

        //Setting up statemachine for my artillery
        stateMachine = new StateMachine();

        //Remember otto you might need to pass more information to your states. Like the heatmap u dumb bitch
        Hunt hunt = new Hunt();
        Search search = new Search();
        
        
        

        //placeing all ships with helperfunction (see below)
        foreach (shipType vessel in myShips)
        {
            placeShip(vessel);
        }
        return myBoard;
        #endregion
    }

    // Helper function to place the ship on designated spot. Different for loops depending on rotation. If rotation not found an exeption is thrown
    private void placeShip(shipType vessel)
    {
        #region Defence
        //Selecting rotaion of ship
        int choseRotation = Random.Range(0, 2);
        rotation shipRotation = choseRotation == 0 ?
            rotation.horizontal :
            rotation.vertical;

        //Choseing a point and validates it with helperfunction below
        Vector2Int anchorPoint = Vector2Int.zero;
        int attempts = 0;
        bool pointOkay = false;

        while (!pointOkay && attempts <= 1000) // FUCK THIS AND ALL IT STANDS FOR
        {
            //Selecting an anchor point randomly with modified x and y values based on rotaion.
            anchorPoint = shipRotation == rotation.horizontal ?
                new Vector2Int(Random.Range(0, fieldSize.x - shipData[vessel]), Random.Range(0, fieldSize.y)) :
                new Vector2Int(Random.Range(0, fieldSize.x), Random.Range(0, fieldSize.y - shipData[vessel]));

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
        #endregion
    }

    private bool validatePoint(Vector2Int point, rotation shipRotation, shipType vessel)
    {
        #region Validator
        //First Check, checks if the point itself is a valid starting point.
        if (myBoard[point.x, point.y])
        {
            return false;
        }

        //Second Check, checks the position of the entire ship
        if (shipRotation == rotation.horizontal)
        {
            for (int i = 0; i < shipData[vessel]; i++)
            {
                if (myBoard[point.x + i, point.y])
                {
                    return false;
                }
            }
        }
        if (shipRotation == rotation.vertical)
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

        int xFrom = Mathf.Max(0, point.x - xSpace);
        int xTo = Mathf.Min(fieldSize.x, point.x + (shipRotation == rotation.horizontal ? shipData[vessel] : 1) + xSpace);

        int yFrom = Mathf.Max(0, point.y - ySpace);
        int yTo = Mathf.Min(fieldSize.y, point.y + (shipRotation == rotation.vertical ? shipData[vessel] : 1) + ySpace);

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
        #endregion
    }

    #region Offence
    /*
    Plan of attack:
    State machine needed with two states. Hunting and Searching. 
    Fire first couple of shots randomly and apply heatmap on an int[,] of the opposing player board
    After set amount of shots, fire att the most probably coordinate and update int[,] based on result.
    int = 0, Miss aka probability is actually 0.
    int = 1, unlikely
    int = 2, medium
    int = 3, probable
    int = 4, Hit
    */
    public Vector2Int Fire()
    {
        throw new System.NotImplementedException();
    }
    /* 
    As soon as i hit something, i want to enter hunt mode in the statemachine. And calculate next firing location
    And when i have sunk that ship, i want to go back into searchmode. 
    */
    public void Result(Vector2Int position, bool hit, bool sunk)
    {
        lastShot = position;
        lastShotHit = hit;
        lastShotSunk = sunk;
    }
    #endregion
} 
