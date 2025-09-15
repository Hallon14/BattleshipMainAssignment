using UnityEngine;

public class AiShitPlayer : IBattleship
{

    bool[,] myPlayField;
    Vector2Int gridSize;

    public string GetName()
    {
        //returns the name of the player, this should be your name!
        //If you don't use your name, you cant win.

        return "Niklas Fischer";
    }

    public bool[,] NewGame(Vector2Int gridSize, string opponentName)
    {
        //This function gets called when we start a new game.
        //return your game field (where you have placed your ships)
        //True for ship and false for blank square.

        //Create our field
        myPlayField = new bool[gridSize.x, gridSize.y];
        //You don't need to do anything with opponent Name
        //this is more if you want to keep track of your
        //opponents names and tactics.
        //we now need to place our ships, lets just do one for the demo.
        //Here is the placement of our battleship.
        myPlayField[3, 2] = true;
        myPlayField[3, 3] = true;
        myPlayField[3, 4] = true;
        myPlayField[3, 5] = true;

        myPlayField[5, 5] = true;
        myPlayField[6, 5] = true;
        myPlayField[7, 5] = true;

        myPlayField[9, 9] = true;
        myPlayField[9, 8] = true;
        //Since we haven't placed all our ships, this would not validate.
        return myPlayField;
    }

    public Vector2Int Fire()
    {
        //When it's your turn to fire, return where you want to fire
        Vector2Int firePosition = new Vector2Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y));

        return firePosition;
    }



    public void Result(Vector2Int position, bool hit, bool sunk)
    {
        //After a hit is resolved the result function will be called.
        //Here your class will get to know if you hit something.

        //Here we don't need to do anything, but it might be a good idea to
        //next to previous hits?
    }

}

