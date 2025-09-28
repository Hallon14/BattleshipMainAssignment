using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

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

    //UI Variables
    private TextMeshProUGUI PlayerFieldText;
    private TextMeshProUGUI AiFieldText;

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
        }
    }

    public void initLevel(LevelData data)
    {
        myField = data.myField;
        AIField = data.AIField;
        baseTile = data.basicTile;
        PlayerFieldText = data.PlayerFieldText;
        AiFieldText = data.AiFieldText;

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
                myField.SetTile(new Vector3Int(x - xSize -1, y, 0), baseTile);
                AIField.SetTile(new Vector3Int(x + 1, y, 0), baseTile);
            }
        }
    }
}

