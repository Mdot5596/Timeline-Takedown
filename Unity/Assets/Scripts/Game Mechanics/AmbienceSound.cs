using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
   public Collider Area;                //The area of the sound
   public GameObject Player;            //The object to track


    void Update()
    {
        //Loacate closest point on the collider to the player
        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);

        //set position to closest point to the player 
        transform.position = closestPoint;

    }
}
