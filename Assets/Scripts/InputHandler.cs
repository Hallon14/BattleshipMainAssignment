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
            shipTile :
            baseTile;

            myField.SetTile(cellPos, newTile);
        }

    }
}
