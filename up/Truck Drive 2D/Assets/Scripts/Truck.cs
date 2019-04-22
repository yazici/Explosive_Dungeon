using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public Engine Truck_Engine;
    public Chassis Truck_Chassis;
}
public abstract class Engine : MonoBehaviour
{
    public int Power;
    public int Mass;
    public bool Wounded_up;
    public int Gasoline_consumption;
    void Update () 
    {
        if(Wounded_up)
    }
}
public abstract class Chassis : MonoBehaviour
{
    
}