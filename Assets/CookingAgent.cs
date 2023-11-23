using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AutoPlayerMovement2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float changeCounterInterval = 2f; // Time interval to change counter
    [SerializeField] private InventoryManager inventoryManager;

    private Rigidbody2D rb;
    private Transform currentCounterTarget;
    private float timeSinceLastCounterChange;

    private Queue<string> counterQueue = new Queue<string>();
    private int maxStamina = 100;
    private int currentStamina;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start with the initial counter order
        InitializeCounterQueue();
        timeSinceLastCounterChange = 0f;

        // Find the InventoryManager script in the scene
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found in the scene.");
        }

        // Initialize stamina
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
        // Check if there is enough stamina to interact with the stove counter
        if (currentStamina > 0)
        {
            FindAndSetCounterTargets();
            // Deduct stamina for interacting with the stove counter
            currentStamina -= 1;
            // Debug log to print the current stamina value
            //Debug.Log("Stamina CookingAgent: " + Mathf.FloorToInt(currentStamina));
        }
        else
        {
            Debug.Log("Cooking Agent is Out of stamina!");
            // You can add additional logic here, such as resetting the game or taking other actions when out of stamina.
        }
    }

    private void InitializeCounterQueue()
    {
        // Define the initial counter order
        counterQueue.Enqueue("PattyCounter");
        counterQueue.Enqueue("StoveCounter");
        counterQueue.Enqueue("CookedPatty");

        // Add more counters or modify the order as needed
    }

    private void FindAndSetCounterTargets()
    {
        if (counterQueue.Count > 0)
        {
            string nextCounterTag = counterQueue.Dequeue();

            if (nextCounterTag == "PattyCounter")
            {
                inventoryManager.MinusPattyInv();
            }
            else if (nextCounterTag == "StoveCounter")
            {
                // Add logic to handle the interaction with the stove counter
                // For example, deducting stamina for the stove agent
                // inventoryManager.DeductStaminaForStove();
                Debug.Log("Interacting with StoveCounter");
            }
            else if (nextCounterTag == "CookedPatty")
            {
                inventoryManager.AddCookedPatty();
            }

            GameObject[] counters = GameObject.FindGameObjectsWithTag(nextCounterTag);
            List<Transform> countersTransforms = counters.Select(counter => counter.transform).ToList();

            if (countersTransforms.Count > 0)
            {
                currentCounterTarget = countersTransforms[Random.Range(0, countersTransforms.Count)];
            }
            else
            {
                Debug.LogWarning("No counters found with tag: " + nextCounterTag);
            }
        }
        else
        {
            InitializeCounterQueue();
        }
    }
}