using UnityEngine;

public class CuttingCounter : MonoBehaviour
{
    [SerializeField] private GameObject ingredientPrefab;

    public void SpawnIngredient(Vector3 spawnPosition)
    {
        Instantiate(ingredientPrefab, spawnPosition, Quaternion.identity);
    }

    // Other methods or variables related to the CuttingCounter can go here
}
