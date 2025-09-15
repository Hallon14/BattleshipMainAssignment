using UnityEngine;

public class BattleShipManager : MonoBehaviour
{
    private IBattleship battlePlayer1;
    private IBattleship battlePlayer2;

    string playerName1;
    string playerName2;
    bool[,] player1PlayField;
    bool[,] player2PlayField;
    [SerializeField] Vector2Int gridSize = new Vector2Int(20,20);

    public GameObject tile;
    public GameObject ship;
    public GameObject hitVis;

    public GameObject[] playerGO = new GameObject[2];

    private void Start()
    {
        battlePlayer1 = new OttWen() as IBattleship;
        battlePlayer2 = new AiShitPlayer() as IBattleship;

        playerName1 = battlePlayer1.GetName();
        playerName2 = battlePlayer2.GetName();

        player1PlayField = battlePlayer1.NewGame(gridSize, playerName2);
        player2PlayField = battlePlayer2.NewGame(gridSize, playerName1);

        SetupGame();
    }

    private void SetupGame()
    {

        playerGO[0] = new GameObject();
        playerGO[1] = new GameObject();
        
        CreatePlaySpaceForPlayer(player1PlayField, playerGO[0].transform);
        CreatePlaySpaceForPlayer(player2PlayField, playerGO[1].transform);

        playerGO[1].transform.position = Vector3.forward * (gridSize.y + 3);
        //CreatePlaySpaceForPlayer(player2PlayField);
    }

    private void CreatePlaySpaceForPlayer(bool[,] playerPlayField, Transform parentTransform)
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            { 
                if (playerPlayField[x, y])
                {
                    Instantiate(ship, new Vector3(x,0f,y),Quaternion.identity, parentTransform);
                }
                else
                {
                    Instantiate(tile, new Vector3(x, 0f, y), Quaternion.identity, parentTransform);
                }
            }
        }
    }



}
