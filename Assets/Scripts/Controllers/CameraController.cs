using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private Vector3 startPosition;
    private float viewportHalfWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)); // the z-component is the distance of the resulting plane from the camera
        startPosition = transform.position;
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - transform.position.x);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position.x - player.position.x;
        startX = transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
        float desiredX = player.position.x + offset;
        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
            transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offset;
        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
            transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);
    }
    public void GameRestart()
    {
        // reset camera position
        transform.position = startPosition;
    }
}
