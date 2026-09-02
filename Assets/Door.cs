using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform door;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 currentPosition;
    private Vector3 startRotation;
    private Vector3 endRotation;
    private Vector3 currentRotation;
    private bool opening = false; //If the door should be opening or closing
    private bool opened = true; //If the door is fully open
    private bool closed = true; //If the door is fully closed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = door.localPosition;
        startRotation = new Vector3(0f, 0f, 0f);
        endRotation = new Vector3(0f, -90f, 0f);
        endPosition = new Vector3(startPosition.x - 2.5f, startPosition.y, startPosition.z - 2.5f); //This is to make the door actually look like it's opening rather than only tilting.
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = door.position;
        currentRotation = door.localEulerAngles;

        if (!opened && opening && currentPosition.x >= endPosition.x && currentPosition.z >= endPosition.z && currentRotation.y >= endRotation.y)
        {
            closed = false;
            if(!opened && (currentPosition.x <= endPosition.x || currentPosition.z <= endPosition.z || currentRotation.y <= endRotation.y))
            {
                currentPosition = endPosition;
                door.localPosition = currentPosition;
                currentRotation = endRotation;
                door.localEulerAngles = currentRotation;
                opened = true;
            } //This stops the door from opening "too far"
            else
            { //This opens the door
                opened = false;
                currentPosition.x -= Time.deltaTime;
                currentPosition.z -= Time.deltaTime;
                door.localPosition = currentPosition;
                currentRotation.y -= Time.deltaTime;
                door.localEulerAngles = currentRotation;
            }
        }
        else if (!closed && !opening && currentPosition.x <= startPosition.x && currentPosition.z <= startPosition.z && currentRotation.y <= startRotation.y)
        {
            opened = false;
            if(!closed && (currentPosition.x >= startPosition.x || currentPosition.z >= startPosition.z || currentRotation.y >= startRotation.y))
            {
                currentPosition = startPosition;
                door.localPosition = currentPosition;
                currentRotation = startRotation;
                door.localEulerAngles = currentRotation;
                closed = true;
            } //This stops the door from closing "too far"
            else
            { //This closes the door
                closed = false;
                currentPosition.x += Time.deltaTime;
                currentPosition.z += Time.deltaTime;
                door.localPosition = currentPosition;
                currentRotation.y += Time.deltaTime;
                door.localEulerAngles = currentRotation;
            }
        }
    }

    void OnEntered() //The Unity event isn't hooked up yet :(
    {
        Debug.Log("Entered!");
        opening = true;
    }

    void OnExited()
    {
        Debug.Log("Exited!");
        opening = false;
    }
}
