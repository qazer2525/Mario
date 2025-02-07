using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class RestartButtonController : MonoBehaviour, IInteractiveButton
{
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
        GameManager.instance.GameRestart();
    }
}