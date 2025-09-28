using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class gameData : MonoBehaviour
{
    [SerializeField]
    private LevelData _levelData = null;

    [SerializeField]
    private GameObject _gameManagerPrefab = null;

    private void Awake()
    {
        if (gameManager.Instance == null)
        {
            Instantiate(_gameManagerPrefab);
        }
        gameManager.Instance.initLevel(_levelData);

    }
}

[System.Serializable]
public class LevelData
{
    //Variables for tilemap
    public Tilemap myField;
    public Tilemap AIField;
    public Tile basicTile;
    public Tile shipTile;

    //UI Variables
    public TextMeshProUGUI PlayerFieldText;
    public TextMeshProUGUI AiFieldText;
}