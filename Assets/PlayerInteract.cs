using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public void InteractWithBox()
    {
        // Your interaction logic here
        Debug.Log("Player interacted with the box!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Counter"))
        {
            // Call the interaction logic when the player enters the trigger collider
            InteractWithBox();
        }
    }
}
