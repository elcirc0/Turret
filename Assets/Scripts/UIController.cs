using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject Player;

    private InputSystem InputSystem;

    public bool UIActionIsActive { get; private set; }

    public static UIController instance;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;

        InputSystem = Player.GetComponent<InputSystem>();
        UI.SetActive(false);

        UIActionIsActive = false;
    }
    public void ShowUI()
    {
        UI.SetActive(true);
        UIActionIsActive = true;
    }

    public void DeactivateInputSystem()
    {
        InputSystem.enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
}
