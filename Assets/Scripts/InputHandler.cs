using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    public Tilemap myField;
    public Tile baseTile;
    public Tile shipTile;
    InputAction interact;

    void Awake()
    {
        interact = InputSystem.actions.FindAction("Battleships/Interact");
    }

    void OnEnable()
    {
        interact.performed += context => SwapTile();
        interact.Enable();
    }

    void OnDisable()
    {
        interact.Disable();
    }



    private void SwapTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPos = myField.WorldToCell(mousePos);

        if (myField.HasTile(cellPos))
        {
            Tile newTile = myField.GetTile(cellPos) == baseTile ?
                shipTile:
                baseTile;

            Debug.Log(cellPos.x + gameManager.Instance.xSize + 1);
            Debug.Log(cellPos.y);
            gameManager.Instance.playerArray[cellPos.x + gameManager.Instance.xSize + 1, cellPos.y] = true;
            myField.SetTile(cellPos, newTile);
        }

    }
}
