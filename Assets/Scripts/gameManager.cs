using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Xml.Serialization;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance = null;

    //Variables needed to play the game
    private Tilemap myField;
    private Tilemap AIField;
    [SerializeField]
    private Tile baseTile;
    [SerializeField]
    private Tile shipTile;

    //Change Axis size in main menu.
    public Slider xSlider;
    public Slider ySlider;
    public TextMeshProUGUI xAxisText;
    public TextMeshProUGUI yAxisText;

    //Variables for x,y Sizes
    private int xSize = 10;
    private int ySize = 10;

    //testcomment

    void Awake()
    {
        // I wanna keep my gameManager into the next scene, so i can keep variables set in the main menu
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log(myField);
            drawGrid();
        }
    
    }

    public void initLevel(LevelData data)
    {
        myField = data.myField;
        AIField = data.AIField;
    }

    //Main Menu Buttons

    public void playGame()
    {
        SceneManager.LoadScene("BattleshipGame");
        drawGrid();
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

    public void drawGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                myField.SetTile(new Vector3Int( x, y, 0), baseTile);
            }
        }
    }
}

