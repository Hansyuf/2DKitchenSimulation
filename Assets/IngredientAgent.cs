using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class IngredientAgent : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float changeCounterInterval = 2f;
    [SerializeField] private float maxStamina = 100f;

    private Rigidbody2D rb;
    private Transform currentCounterTarget;
    private float timeSinceLastCounterChange;
    private Queue<string> counterQueue = new Queue<string>();
    private float currentStamina;

    [SerializeField] private InventoryManager inventoryManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventoryManager = GetComponent<InventoryManager>();

        InitializeCounterQueue();
        timeSinceLastCounterChange = 0f;
        currentStamina = maxStamina;

        // Find the InventoryManager script in the scene
        inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not found in the scene.");
        }
    }

    private void Update()
    {
        AutoMovePlayer();
        timeSinceLastCounterChange += Time.deltaTime;

        // Deduct stamina over time
        currentStamina -= Time.deltaTime;

        if (timeSinceLastCounterChange >= changeCounterInterval)
        {
            SwitchCounterTarget();
            timeSinceLastCounterChange = 0f;
        }
    }

    private void AutoMovePlayer()
    {
        if (currentCounterTarget != null)
        {
            Vector2 moveDirection = (currentCounterTarget.position - transform.position).normalized;
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    private void SwitchCounterTarget()
    {
        // Check if there is enough stamina to interact with the cutting counter
        if (currentStamina > 0)
        {
            FindAndSetCounterTargets();
            // Deduct stamina for interacting with the cutting counter
            currentStamina -= 1;

            // Debug log to print the current stamina value as a whole number
            //Debug.Log("Stamina IngredientAgent: " + Mathf.FloorToInt(currentStamina));
        }
        else
        {
            Debug.Log("Ingredient agent is Out of stamina!");
            // You can add additional logic here, such as resetting the game or taking other actions when out of stamina.
        }
    }

    private void InitializeCounterQueue()
    {
        // Define the initial counter order
        counterQueue.Enqueue("CheeseCounter");
        counterQueue.Enqueue("CuttingCounter");
        counterQueue.Enqueue("CutCheese");
        counterQueue.Enqueue("CheeseCounter"); // Move back to CheeseCounter

        counterQueue.Enqueue("LettuceCounter");
        counterQueue.Enqueue("CuttingCounter");
        counterQueue.Enqueue("CutLettuce");
        counterQueue.Enqueue("LettuceCounter"); // Move back to LettuceCounter

        counterQueue.Enqueue("TomatoCounter");
        counterQueue.Enqueue("CuttingCounter");
        counterQueue.Enqueue("CutTomato");
        counterQueue.Enqueue("TomatoCounter"); // Move back to TomatoCounter

        // Add more counters or modify the order as needed
    }

    private void FindAndSetCounterTargets()
    {
        if (counterQueue.Count > 0)
        {
            string nextCounterTag = counterQueue.Dequeue();

            if (nextCounterTag == "CutCheese")
            {
                inventoryManager.AddCutCheese();
            }
            else if (nextCounterTag == "CutLettuce")
            {
                inventoryManager.AddCutLettuce();
            }
            else if (nextCounterTag == "CutTomato")
            {
                inventoryManager.AddCutTomato();
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