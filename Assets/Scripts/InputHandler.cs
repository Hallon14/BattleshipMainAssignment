using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance = null;
    public Tilemap myField;
    public Tile baseTile;
    public Tile shipTile;
    public Tile hitTile;
    public Tile missTile;
    InputAction interact;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        interact = InputSystem.actions.FindAction("Battleships/Interact");
    }

    void OnEnable()
    {
        //interact.performed += OnInteract();
        interact.Enable();
    }

    void OnDisable()
    {
        interact.Disable();
    }

    void OnInteract(InputAction.CallbackContext contrext)
    {
        SwapTile();
    }

    private void SwapTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPos = myField.WorldToCell(mousePos);

        if (myField.HasTile(cellPos))
        {
            Tile newTile = myField.GetTile(cellPos) == baseTile ?
                shipTile :
                baseTile;

            Debug.Log(cellPos.x + gameManager.Instance.xSize + 1);
            Debug.Log(cellPos.y);
            gameManager.Instance.playerArray[cellPos.x + gameManager.Instance.xSize + 1, cellPos.y] = true;
            myField.SetTile(cellPos, newTile);
        }
    }

    public void placeHitTile(Vector3Int location, Tilemap gridToDrawOn)
    {
        if (gridToDrawOn.HasTile(location))
        {
            gridToDrawOn.SetTile(location, hitTile);
        }
    }
    public void placeMissTile(Vector3Int location, Tilemap gridToDrawOn)
    {
        if (gridToDrawOn.HasTile(location))
        {
            gridToDrawOn.SetTile(location, hitTile);
        }
    }
}
