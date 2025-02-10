using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class RestartButtonController : MonoBehaviour, IInteractiveButton


{
    public UnityEvent gameRestart;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ButtonClick()
    {
        gameRestart.Invoke();
    }
}