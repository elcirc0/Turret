using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    [SerializeField] InputActionAsset InputAction;
    [SerializeField] InputActionAsset InputActionUI;

    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private Transform LeftAndRight;
    [SerializeField] private Transform UpAndDown;

    private InputAction lookAction;
    private InputAction touchUIAction;

    //private bool touchUIActionIsActive = false;

    private Vector2 lookPos;

    private void OnEnable()
    {
        InputAction.FindActionMap("Turret").Enable();
        //InputActionUI.FindActionMap("UI").Enable();
    }

    private void OnDisable()
    {
        InputAction.FindActionMap("Turret").Disable();
        //InputActionUI.FindActionMap("UI").Disable();
    }

    private void Awake()
    {
        lookAction = UnityEngine.InputSystem.InputSystem.actions.FindAction("Look");
        touchUIAction = UnityEngine.InputSystem.InputSystem.actions.FindAction("Click");
    }

    private void Update()
    {
        lookPos = lookAction.ReadValue<Vector2>();

        //if (UIController.instance.UIActionIsActive) { touchUIAction.ReadValueAsObject(); }
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
