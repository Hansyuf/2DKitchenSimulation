using UnityEngine;

public class InventoryManager : MonoBehaviour

{
    //Cutted Ingredient Inventory (All these var will be increase)
    public int cutCheeseInventory = 5;
    public int cutLettuceInventory = 5;
    public int cutTomatoInventory = 5;
    public int CookedPattyInventory = 0;
    


    //Counter Inventory (All these counter is decrease)
    public int PattyCounterInventory = 50; 
    public int BunCounterInventory = 50; 
    public int CheeseCounterInventory = 50; 
    public int LettuceCounterInventory = 50; 
    public int TomatoCounterInventory = 50; 
    
    //cheese
    public void AddCutCheese()
    {
        if (cutCheeseInventory < 5)
        {
            cutCheeseInventory++;
            Debug.Log("CutCheese Inventory: " + cutCheeseInventory);
        }
    }
    public void MinusCheeseInv(){
    CheeseCounterInventory--;
    Debug.Log("Cheese Inventory: " + CheeseCounterInventory);
   }
    public void MinusCutCheese(){
    cutCheeseInventory--;
    Debug.Log("Cut Cheese Inventory: " + cutCheeseInventory);
   }
    
    
    
    //Lettuce
    public void AddCutLettuce()
    {
        if (cutLettuceInventory < 5)
        {
            cutLettuceInventory++;
            Debug.Log("CutLettuce Inventory: " + cutLettuceInventory);
        }
    }
    public void MinusLettuceInv(){
        LettuceCounterInventory--;
        //Debug.Log("Lettuce Inventory: " + LettuceCounterInventory);
   }
     public void MinusCutLettuce(){
        cutLettuceInventory--;
        //Debug.Log("Cut Lettuce Inventory: " + cutLettuceInventory);
   }
    



    
    //Tomato
    public void AddCutTomato()
    {
        if (cutTomatoInventory < 5)
        {

            cutTomatoInventory++;
            //Debug.Log("CutTomato Inventory: " + cutTomatoInventory);
        }
    }
    public void MinusTomatoInv(){
        TomatoCounterInventory--;
        //Debug.Log("Tomato Inventory: " + TomatoCounterInventory);
   }    
    public void MinusCutTomato(){
        cutTomatoInventory--;
        //Debug.Log("Cut Tomato Inventory: " + cutTomatoInventory);
   }    
    
    //Bun
    public void MinusBunInv()
    {
            BunCounterInventory--;
            //Debug.Log("Bun Inventory: " + BunCounterInventory);
       
    }
    
    //Patty
    public void AddCookedPatty()
    {
        if (CookedPattyInventory < 5)
        {

            PattyCounterInventory--;
            CookedPattyInventory++;
            //Debug.Log("CookedPatty Inventory: " + CookedPattyInventory);
        }
    }
    public void MinusPattyInv(){

        PattyCounterInventory--;
        //Debug.Log("Patty Inventory: " + PattyCounterInventory);
   }    
    public void MinusCookedPatty(){

        CookedPattyInventory--;
        //Debug.Log("Cooked Patty: " + CookedPattyInventory);
   }  




}