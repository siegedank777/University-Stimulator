using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    public Tilemap map;
    [SerializeField] private float movementSpeed;

    MouseInput mouseInput;

    private Vector3 destination;

    private void Awake()
    {
        mouseInput = new MouseInput();
    }

    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
    }

    private void MouseClick()
    {
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition); //캐릭터가 타일 밖을 클릭해도 안나가게 만들시 필요함.
        if(map.HasTile(gridPosition))
        {
            destination = mousePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.paused) return;
        if(Vector3.Distance(transform.position, destination)>0.1f)
        transform.position = Vector3.MoveTowards(transform.position/*현재위치*/,destination,movementSpeed*Time.deltaTime);
    }
}
