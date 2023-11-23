using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AutoPlayerMovement1 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float changeCounterInterval = 2f; // Time interval to change counter
    [SerializeField] private int maxStamina = 100; // Maximum stamina

    private Rigidbody2D rb;
    private Transform currentCounterTarget;
    private float timeSinceLastCounterChange;
    private Queue<string> counterQueue = new Queue<string>();
    private int cutCheeseInventory = 5;
    private int cutLettuceInventory = 5;
    private int cutTomatoInventory = 5;
    private int cookedPatty = 5;
    private int PairBun = 50;
    private int DeliveryCount = 0;
    private int currentStamina; // Current stamina

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start with the initial counter order
        InitializeCounterQueue();
        timeSinceLastCounterChange = 0f;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        AutoMovePlayer();

        // Check if it's time to change counter
        timeSinceLastCounterChange += Time.deltaTime;
        if (timeSinceLastCounterChange >= changeCounterInterval)
        {
            // Change counter target
            SwitchCounterTarget();
            timeSinceLastCounterChange = 0f;
        }
    }

    private void AutoMovePlayer()
    {
        // Move the player towards the current counter target
        if (currentCounterTarget != null)
        {
            Vector2 moveDirection = (currentCounterTarget.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    private void SwitchCounterTarget()
    {
        // Check if there is enough stamina to interact with the delivery counter
        if (currentStamina > 0)
        {
            FindAndSetCounterTargets();
            // Deduct stamina for interacting with the delivery counter
            currentStamina -= 1;
            // Debug log to print the current stamina value
            //Debug.Log("Stamina DeliveryAgent: " + Mathf.FloorToInt(currentStamina));
        }
        else
        {
            Debug.Log("Delivery Agent is Out of stamina!");
            // You can add additional logic here, such as resetting the game or taking other actions when out of stamina.
        }
    }

    private void InitializeCounterQueue()
    {
        // Define the initial counter order
        counterQueue.Enqueue("PlateCounter");
        counterQueue.Enqueue("CutCheese");
        counterQueue.Enqueue("CutLettuce");
        counterQueue.Enqueue("CutTomato");
        counterQueue.Enqueue("BunCounter");
        counterQueue.Enqueue("CookedPatty");
        counterQueue.Enqueue("DeliveryCounter");
        // Add more counters or modify the order as needed
    }

    private void FindAndSetCounterTargets()
    {
        if (counterQueue.Count > 0)
        {
            string nextCounterTag = counterQueue.Dequeue();

            if (nextCounterTag == "PlateCounter")
            {
                //Debug.Log("Took Plate");
            }
            else if (nextCounterTag == "CutCheese")
            {
                cutCheeseInventory--;
                //Debug.Log("CutCheese Inventory: " + cutCheeseInventory);
            }
            else if (nextCounterTag == "CutLettuce")
            {
                cutLettuceInventory--;
                //Debug.Log("CutLettuce Inventory: " + cutLettuceInventory);
            }
            else if (nextCounterTag == "CutTomato")
            {
                cutTomatoInventory--;
                //Debug.Log("CutTomato Inventory: " + cutTomatoInventory);
            }
            else if (nextCounterTag == "DeliveryCounter")
            {
                DeliveryCount++;
                Debug.Log("Total Delivery : " + DeliveryCount);
            }

            GameObject[] counters = GameObject.FindGameObjectsWithTag(nextCounterTag);
            List<Transform> countersTransforms = counters.Select(counter => counter.transform).ToList();

            // Check if countersTransforms is not empty before accessing elements
            if (countersTransforms.Count > 0)
            {
                currentCounterTarget = countersTransforms[Random.Range(0, countersTransforms.Count)];
            }
            else
            {
                // Handle the case when countersTransforms is empty
                Debug.LogWarning("No counters found with tag: " + nextCounterTag);
                // You might want to handle this situation differently based on your game's logic
            }
        }
        else
        {
            // If the queue is empty, initialize it again
            InitializeCounterQueue();
        }
    }
}