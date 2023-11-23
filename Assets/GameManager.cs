using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private OrderQueue orderQueue;

    void Start()
    {
        // Get reference to the OrderQueue script
        orderQueue = GetComponent<OrderQueue>();

        // Subscribe to events
        orderQueue.OrderGenerated += OnOrderGenerated;
        orderQueue.OrderCompleted += OnOrderCompleted;
    }

    void OnOrderGenerated(object sender, OrderQueue.OrderEventArgs e)
    {
        // Handle the event when a new order is generated
        Debug.Log("New order generated: " + e.CompletedOrder);
    }

    void OnOrderCompleted(object sender, OrderQueue.OrderEventArgs e)
    {
        // Handle the event when an order is completed
        Debug.Log("Order completed: " + e.CompletedOrder);
    }

    void Update()
    {
        // Check for user input or other game logic to complete orders
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CompleteOrder();
        }
    }

    void CompleteOrder()
    {
        // Simulate completing an order
        orderQueue.CompleteOrder();
    }
}
