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
        
    }

    public void Start()
    {
        gameManager.Instance.initLevel(_levelData);
    }
}

[System.Serializable]
public class LevelData
{
    public Tilemap myField;
    public Tilemap AIField;
}