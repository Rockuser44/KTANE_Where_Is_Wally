using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class WhereIsWallyScript : MonoBehaviour {

   //public variables
   public KMBombInfo bomb;
   public KMAudio audio;

   //Add buttons to selectable list
   public KMSelectable buttonDown;     
   public KMSelectable buttonUp;     
   public KMSelectable buttonLeft;     
   public KMSelectable buttonRight;     
   public KMSelectable buttonCentre;   

   public Renderer Picture;  

   //Image Arrays
   public Material[] BeachIncorrectImagesArray;
   public Material[] BeachCorrectImagesArray;
   public Material[] GreekImagesArray;
      /*/Index is the following locations:
         0 = A1
         1 = A2
         2 = A3
         3 = A4
         4 = B1
         5 = B2
         6 = B3
         7 = B4
         8 = C1
         9 = C2
         10 = C3
         11 = C4
         12 = D1
         13 = D2
         14 = D3
         15 = D4
         16 = E1
         17 = E2
         18 = E3
         19 = E4
         20 = F1
         21 = F2
         22 = F3
         23 = F4
      /*/
   //int[] correctLocationArray = {13,3,0};
   /*/Array with the correct location for each Image array as follows:
         0 = 13   (D2 for beach image)
         1 = 3    (A4 for greeks image)
   /*/

   string[] location = {"A1","A2","A3","A4","B1","B2","B3","B4","C1","C2","C3","C4","D1","D2","D3","D4","E1","E2","E3","E4","F1","F2","F3","F4"};
   //String array of location names used for logging

   //Private variables
   private int currentImageIndex = 0;
   private int currentLocationIndex = 0;
   private int correctLocationIndex = 0;



   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;


   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void Awake () {
      ModuleId = ModuleIdCounter++;

      // Make functions called PressButtonX occur when you press buttonX
      buttonDown.OnInteract   += delegate () { PressButtonDown();    return false; }; 
      buttonUp.OnInteract     += delegate () { PressButtonUp();      return false; }; 
      buttonLeft.OnInteract   += delegate () { PressButtonLeft();    return false; };
      buttonRight.OnInteract  += delegate () { PressButtonRight();   return false; };    
      buttonCentre.OnInteract += delegate () { PressButtonCentre(); return false; };   


   }
   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void Start () {

   
      
      //Determine which image you are using
      //currentImageIndex = UnityEngine.Random.Range(0,2);
      currentImageIndex = 0;  //force value for testing

      //Dynamically generate a correct image for Wally
      correctLocationIndex = UnityEngine.Random.Range(0,24);
      //correctLocationIndex = 1;  //force value for testing

      Debug.LogFormat("[WhereIsWally #{0}] Wally can be found at {1}.", ModuleId, location[correctLocationIndex]);

      //Make random starting location
      currentLocationIndex = UnityEngine.Random.Range(0,24);

      //Set first image
      RefreshImage();
      
   }
   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void Update () {
      //Currently does nothing
   }
   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void PressButtonDown()
   {    //Pressed survivor 0
            //Log press
            Debug.LogFormat("[WhereIsWally #{0}] You pressed the down button.", ModuleId);

         //Move the current index
            //If bottom of picture don't move down
            if(currentLocationIndex % 4 == 3){
               Debug.LogFormat("[WhereIsWally #{0}] Cannot move down.", ModuleId);
               return;
            }
            else{
               //Otherwise, do move down
               currentLocationIndex = currentLocationIndex + 1;
               //And refresh the picture
               RefreshImage();
            }

     
   }
   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void PressButtonUp()
   {    //Pressed survivor 1
            //Log press
            Debug.LogFormat("[WhereIsWally #{0}] You pressed the up button.", ModuleId);

         //Move the current index
            //If bottom of picture don't move up
            if(currentLocationIndex % 4 == 0){
               Debug.LogFormat("[WhereIsWally #{0}] Cannot move up.", ModuleId);
               return;
            }
            else{
               //Otherwise, do move up
               currentLocationIndex = currentLocationIndex - 1;
               //And refresh the picture
               RefreshImage();
            }

     
   }
   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void PressButtonLeft()
   {  //Pressed survivor 2
         //Log press
         Debug.LogFormat("[WhereIsWally #{0}] You pressed the left button.", ModuleId);
      
         //Move the current index
            //If left of picture don't move left
            if(currentLocationIndex <= 3){
               Debug.LogFormat("[WhereIsWally #{0}] Cannot move left.", ModuleId);
               return;
            }
            else{
               //Otherwise, do move left
               currentLocationIndex = currentLocationIndex - 4;
               //And refresh the picture
               RefreshImage();
            }
      
   }
   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void PressButtonRight()
   {  //Pressed survivor 3
         //Log press
         Debug.LogFormat("[WhereIsWally #{0}] You pressed the right button.", ModuleId);
      
         //Move the current index
            //If left of picture don't move right
            if(currentLocationIndex >= 20){
               Debug.LogFormat("[WhereIsWally #{0}] Cannot move right.", ModuleId);
               return;
            }
            else{
               //Otherwise, do move right
               currentLocationIndex = currentLocationIndex + 4;
               //And refresh the picture
               RefreshImage();
            } 
      
   }
   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void PressButtonCentre()
   {  //Pressed killer icon
         //Log press
         Debug.LogFormat("[WhereIsWally #{0}] You pressed the centre button.", ModuleId);
      
     
         if(currentLocationIndex == correctLocationIndex){
            Debug.LogFormat("[WhereIsWally #{0}] Wally has been found!", ModuleId);
               GetComponent<KMBombModule>().HandlePass();
               
            }
         else{
            Debug.LogFormat("[WhereIsWally #{0}] No Wally here!", ModuleId);
               GetComponent<KMBombModule>().HandleStrike();
            }

         
      

     
   }
   //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
   void RefreshImage()
   {  //Refresh the pictures image
         
      Debug.LogFormat("[WhereIsWally #{0}] Current location is {1}.", ModuleId, location[currentLocationIndex]);

      //Beach Images
      if(currentImageIndex == 0){
               if(currentLocationIndex == correctLocationIndex){
                  Picture.sharedMaterial = BeachCorrectImagesArray[currentLocationIndex];
                  return;
               }
               else
               Picture.sharedMaterial = BeachIncorrectImagesArray[currentLocationIndex];
               return;
            }
      //Greeks Images      
      else if(currentImageIndex == 1){
               Picture.sharedMaterial = GreekImagesArray[currentLocationIndex];
               return;
            }
      
     
   }
  
      
}



