using System;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    // Enum to represent different menu items
    public enum OrderItem
    {
        Salad,
        BurgerBishe,
        BurgerCheese,
        BurgerBahagia
    }

    // Dictionary to store the required ingredients for each menu item
    private Dictionary<OrderItem, List<string>> order = new Dictionary<OrderItem, List<string>>
    {
        { OrderItem.Salad, new List<string> { "Salad", "Tomato" } },
        { OrderItem.BurgerBishe, new List<string> { "Bun", "Patty", "Salad", "Tomato" } },
        { OrderItem.BurgerCheese, new List<string> { "Bun", "Patty", "Cheese" } },
        { OrderItem.BurgerBahagia, new List<string> { "Bun", "Patty", "Cheese", "Salad", "Tomato" } }
    };

    // Fields to store the current order item and ingredients
    private OrderItem currentOrderItem;
    private List<string> currentIngredients;

    // Method to get the ingredients for a specific menu item
    public List<string> GetIngredients(OrderItem orderItem)
    {
        if (order.ContainsKey(orderItem))
        {
            return order[orderItem];
        }

        return null;
    }

    // Method to set the order item
    public void SetOrderItem(OrderItem orderItem)
    {
        currentOrderItem = orderItem;
    }

    // Method to set the ingredients
    public void SetIngredients(List<string> ingredients)
    {
        currentIngredients = ingredients;
    }

    // Method to get the current order item
    public OrderItem GetCurrentOrderItem()
    {
        return currentOrderItem;
    }

    // Method to get the current ingredients
    public List<string> GetCurrentIngredients()
    {
        return currentIngredients;
    }

    // New method to get the counter tag for each ingredient in the current order
    public Dictionary<string, string> GetIngredientToCounterMapping()
    {
        Dictionary<string, string> ingredientToCounterMapping = new Dictionary<string, string>();

        if (currentIngredients != null)
        {
            foreach (string ingredient in currentIngredients)
            {
                // Customize this logic based on your game's counter tagging
                string counterTag = GetCounterTagForIngredient(currentOrderItem, ingredient);
                ingredientToCounterMapping.Add(ingredient, counterTag);
            }
        }

        return ingredientToCounterMapping;
    }

    // Customize this method based on your game's logic for counter tagging
    private string GetCounterTagForIngredient(OrderItem orderItem, string ingredient)
    {
        // Example logic: assuming each ingredient goes to a counter with a matching tag
        switch (orderItem)
        {
            case OrderItem.Salad:
                switch (ingredient.ToLower())
                {
                    case "lettuce":
                        return "LettuceCounter";
                    case "tomato":
                        return "TomatoCounter";
                    // Add more cases for other ingredients as needed
                    default:
                        return "DefaultCounter";
                }

           case OrderItem.BurgerBishe:
            switch (ingredient.ToLower())
            {
                case "bun":
                    return "BunCounter";
                case "patty":
                    return "PattyCounter";
                case "salad":
                    return "SaladCounter";
                case "tomato":
                    return "TomatoCounter";
                // Add more cases for other BurgerBishe ingredients as needed
                default:
                    return "DefaultCounter";
            }

        case OrderItem.BurgerCheese:
            switch (ingredient.ToLower())
            {
                case "bun":
                    return "BunCounter";
                case "patty":
                    return "PattyCounter";
                case "cheese":
                    return "CheeseCounter";
                // Add more cases for other BurgerCheese ingredients as needed
                default:
                    return "DefaultCounter";
            }

        case OrderItem.BurgerBahagia:
            switch (ingredient.ToLower())
            {
                case "bun":
                    return "BunCounter";
                case "patty":
                    return "PattyCounter";
                case "cheese":
                    return "CheeseCounter";
                case "salad":
                    return "SaladCounter";
                case "tomato":
                    return "TomatoCounter";
                // Add more cases for other BurgerBahagia ingredients as needed
                default:
                    return "DefaultCounter";
            }

        // Add more cases for other OrderItems as needed
    }

    return "DefaultCounter";
}
}
