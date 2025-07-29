using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    [SerializeField] InputActionAsset InputAction;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private Transform LeftAndRight;
    [SerializeField] private Transform UpAndDown;

    private InputAction lookAction;
    private Vector2 lookPos;

    private void OnEnable()
    {
        InputAction.FindActionMap("Turret").Enable();
    }

    private void OnDisable()
    {
        InputAction.FindActionMap("Turret").Disable();
    }

    private void Awake()
    {
        lookAction = UnityEngine.InputSystem.InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        lookPos = lookAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    private void Rotation()
    {
        float LeftAndRightAmount = lookPos.x * rotationSpeed * Time.deltaTime;
        float UpAndDownAmount = lookPos.y * rotationSpeed * Time.deltaTime;
        Quaternion LeftAndRightRotation = Quaternion.Euler(0, LeftAndRightAmount, 0);
        Quaternion UpAndDownRotation = Quaternion.Euler(-UpAndDownAmount, 0, 0);
        LeftAndRight.rotation *= LeftAndRightRotation;
        UpAndDown.rotation *= UpAndDownRotation;
    }
}
