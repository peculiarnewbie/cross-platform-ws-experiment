using UnityEngine;


public class SocketControlled : MonoBehaviour
{

    bool isMovingUp = false;
    bool isMovingDown = false;

    private void Update()
    {
        if (isMovingUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (isMovingDown)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }
    }

    public void StartMovingUp()
    {
        isMovingUp = true;
    }

    public void StopMovingUp()
    {
        isMovingUp = false;
        // transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    public void StartMovingDown()
    {
        isMovingDown = true;
    }

    public void StopMovingDown()
    {
        isMovingDown = false;
        // transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

}
