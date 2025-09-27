using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class gameManager : MonoBehaviour
{

    //Variables needed to play the game
    private Tilemap MyField;
    private Tilemap AIField;

    //Change Axis size in main menu.
    public Slider xSlider;
    public Slider ySlider;
    public TextMeshProUGUI xAxisText;
    public TextMeshProUGUI yAxisText;

    //Variables for x,y Sizes
    private int xSize = 10;
    private int ySize = 10;


    void Awake()
    {
        // I wanna keep my gameManager into the next scene, so i can keep variables set in the main menu
        DontDestroyOnLoad(this);
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
        public void changeYAxis() {
        yAxisText.text = "y Axis set to: " + ySlider.value.ToString();
        ySize = (int)ySlider.value;
    }

}

