using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderQueue : MonoBehaviour
{
    private static OrderQueue instance; // Static instance for easy access

    public static OrderQueue Instance
    {
        get { return instance; }
    }

    private Queue<Order> orderQueue = new Queue<Order>();

    public event EventHandler<OrderEventArgs> OrderCompleted;
    public event EventHandler<OrderEventArgs> OrderGenerated;

    public class OrderEventArgs : EventArgs
    {
        public Order CompletedOrder { get; set; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    private void Start()
    {
        // Start generating random orders
        StartCoroutine(GenerateRandomOrdersCoroutine());
    }

    private IEnumerator GenerateRandomOrdersCoroutine()
    {
        while (true)
        {
            // Create a new instance of the Order class
            Order orderInstance = new Order();

            // Generate a new random order item
            Order.OrderItem randomOrderItem = (Order.OrderItem)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Order.OrderItem)).Length);

            // Get the ingredients for the selected order item using the order instance
            List<string> ingredients = orderInstance.GetIngredients(randomOrderItem);

            // Set the order item and ingredients in the order instance
            orderInstance.SetOrderItem(randomOrderItem);
            orderInstance.SetIngredients(ingredients);

            // Enqueue the new order
            EnqueueOrder(orderInstance);

            // Log information about the generated order
            Debug.Log("Generated Order - Item: " + randomOrderItem + ", Ingredients: " + string.Join(", ", ingredients) + " ,Order count:" + orderQueue.Count);

            // Generate a random time interval between 2 and 10 seconds 
            float randomInterval = UnityEngine.Random.Range(2f, 10f);

            // Wait for the random interval
            yield return new WaitForSeconds(randomInterval);
        }
    }

    public void EnqueueOrder(Order order)
    {
        orderQueue.Enqueue(order);
        OrderGenerated?.Invoke(this, new OrderEventArgs { CompletedOrder = order });
    }

    public Order DequeueOrder()
    {
        if (orderQueue.Count > 0)
        {
            return orderQueue.Dequeue();
        }

        return null;
    }

    public Order PeekNextOrder()
    {
        if (orderQueue.Count > 0)
        {
            return orderQueue.Peek();
        }

        return null;
    }

    public bool IsQueueEmpty()
    {
        return orderQueue.Count == 0;
    }

    public void CompleteOrder()
    {
        Order completedOrder = DequeueOrder();
        if (completedOrder != null)
        {
            OrderCompleted?.Invoke(this, new OrderEventArgs { CompletedOrder = completedOrder });
        }
    }
}
