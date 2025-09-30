using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class InputHandler : MonoBehaviour
{
    public Tilemap myField;
    public Tilemap enemyField;

    public Tile baseTile;
    public Tile shipTile;

    InputAction interact;
    public GameObject validateButton;

    //Setup
    void Awake()
    {
        interact = InputSystem.actions.FindAction("Battleships/Interact");
    }

    void OnEnable()
    {
        interact.performed += OnSetupInteract;
        interact.Enable();
    }

    void OnDisable()
    {
        interact.Disable();
    }

    private void OnSetupInteract(InputAction.CallbackContext context)
    {
        SwapTile();
    }

    private void OnPlayingInteract(InputAction.CallbackContext context)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPos = myField.WorldToCell(mousePos);

    }

    private void SwapTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPos = myField.WorldToCell(mousePos);

        if (myField.HasTile(cellPos))
        {
            Tile newTile = myField.GetTile(cellPos) == baseTile ?
                placeShipTile(cellPos) :
                placeBaseTile(cellPos);
        }
    }

    private Tile placeShipTile(Vector3Int cellPos)
    {
        gameManager.Instance.playerArray[cellPos.x + gameManager.Instance.xSize + 1, cellPos.y] = true;
        myField.SetTile(cellPos, shipTile);
        return shipTile;
    }

    private Tile placeBaseTile(Vector3Int cellPos)
    {
        gameManager.Instance.playerArray[cellPos.x + gameManager.Instance.xSize + 1, cellPos.y] = false;
        myField.SetTile(cellPos, baseTile);
        return baseTile;
    }


    public void ValidatePlayer()
    {
        Debug.Log("Maybe not valid but if Otto is playing he knows the rules");
        interact.performed -= OnSetupInteract;
        interact.performed += OnPlayingInteract;
        validateButton.SetActive(false);

    }
}
