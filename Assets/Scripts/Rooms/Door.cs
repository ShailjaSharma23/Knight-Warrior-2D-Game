using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform prevRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;


void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.CompareTag("Player"))
    {
        if (collision.transform.position.x < transform.position.x)
        {
            cam.MoveToNewRoom(nextRoom);
            nextRoom.GetComponent<Room>().ActivateRoom(true);
            prevRoom.GetComponent<Room>().ActivateRoom(false);
        }
        else
        {
            cam.MoveToNewRoom(prevRoom);
            prevRoom.GetComponent<Room>().ActivateRoom(true);
            nextRoom.GetComponent<Room>().ActivateRoom(false);
        }
    }
}
}