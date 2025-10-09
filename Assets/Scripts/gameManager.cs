using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections.Generic;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance = null;
    private OttWen aiPlayer;
    private HumanPlayer humanPlayer;
    private bool gameIsPlaying;

    //Variables needed to play the game
    private Tilemap myField;
    private Tilemap AIField;
    public bool[,] playerArray;
    public bool[,] AiArray;

    //Tiles (might not be needed, move to Input Hanlder if possible)
    [SerializeField]
    private Tile baseTile;
    [SerializeField]
    private Tile shipTile;
    [SerializeField]
    private Tile hitTile;
    [SerializeField]
    private Tile missTile;

    //Change Axis size in main menu.
    public Slider xSlider;
    public Slider ySlider;
    public TextMeshProUGUI xAxisText;
    public TextMeshProUGUI yAxisText;

    //Variables for x,y Sizes
    public int xSize = 10;
    public int ySize = 10;

    //Hashsets for the player and ai
    private HashSet<Vector2Int> aiHits;
    private HashSet<Vector2Int> aiMisses;
    private HashSet<Vector2Int> playerHits;
    private HashSet<Vector2Int> playerMisses;


    //UI Variables
    private TextMeshProUGUI PlayerFieldText;
    private TextMeshProUGUI AiFieldText;

    //Sort these 
    private bool aiHit;
    private Button validateButton;

    //Setup for the game manager
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        if (scene.buildIndex == 1)
        {
            drawGrid();
            moveCamera.Instance.moveCameraToFit();
            aiPlayer = new OttWen();

            humanPlayer = new HumanPlayer();
            playerArray = new bool[xSize, ySize];

            AiArray = aiPlayer.NewGame(new Vector2Int(xSize, ySize), "Not assigned");
            playerArray = humanPlayer.NewGame(xSize, ySize);

            validateButton.onClick.AddListener(validatePlayer);
        }
    }

    public void initLevel(LevelData data)
    {
        myField = data.myField;
        AIField = data.AIField;
        baseTile = data.basicTile;
        PlayerFieldText = data.PlayerFieldText;
        AiFieldText = data.AiFieldText;
        hitTile = data.hitTile;
        missTile = data.missTile;

    }

    //Main Menu Buttons
    public void playGame()
    {
        SceneManager.LoadScene("BattleshipGame");
    }

    public void exitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif   
    }

    public void changeXAxis()
    {
        xAxisText.text = "x Axis set to: " + xSlider.value.ToString();
        xSize = (int)xSlider.value;
    }
    public void changeYAxis()
    {
        yAxisText.text = "y Axis set to: " + ySlider.value.ToString();
        ySize = (int)ySlider.value;
    }

    //Functions for the game
    public void drawGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                myField.SetTile(new Vector3Int(x - xSize - 1, y, 0), baseTile);
                AIField.SetTile(new Vector3Int(x + 1, y, 0), baseTile);
            }
        }
    }
    private void validatePlayer()
    {
        Debug.Log("Might not be valid, but if otto is playing he knows the rules");
        // Turn off inputs on player tilemap InputHandler.Instance.
        validateButton.onClick.RemoveListener(validatePlayer);
        Destroy(validateButton);
        playBattleships();

    }
    public void playBattleships()
    {
        gameIsPlaying = true;
        while (gameIsPlaying)
        {
            aiTurn();
            playerTurn();
        }
    }

    private void playerTurn()
    {
        throw new NotImplementedException();
    }

    public void aiTurn()
    {
        Vector2Int lastAiShot = aiPlayer.Fire();
        aiHit = false;
        if (playerArray[lastAiShot.x, lastAiShot.y])
        {
            aiHit = true;
            InputHandler.Instance.placeHitTile((Vector3Int)lastAiShot, myField);
        }
        else
        {
            InputHandler.Instance.placeMissTile((Vector3Int)lastAiShot, myField);
        }
        
    }

    public bool sunkCheck(Vector2Int shot, bool[,] gridToCheck)
    {
        for (int x = shot.x - 4; x <= shot.x + 4; x++)
        {
            for (int y = shot.y - 4; y <= shot.y - 4; y++)
            {
                try
                {
                    if (gridToCheck[shot.x, shot.y] == true && !aiHits.Contains(shot))
                    {
                        return false;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }
        }
        return true;
    }
}

