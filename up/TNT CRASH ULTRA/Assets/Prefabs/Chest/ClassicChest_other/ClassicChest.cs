using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicChest : MonoBehaviour
{
    public GameObject[] ChestPrefabs; // Префабы сундуков
    public Transform Spawner;
    public static GameObject ClonedChest; // 
    private void Start()
    {
        InvokeRepeating("SpawnChest", 4f, 20f);
    }
    public enum Chest { Classic, Diamond, Rainbow, Glitch }
    public Chest RandomiseChest() // Classic - 50%; Diamond - 30%; Rainbow - 15%; Glitch - 5%;
    {
        int chance = Random.Range(0, 101);
        Chest returned;
        if (chance <= 50)
        { returned = Chest.Classic; }
        else if (chance <= 80 && chance > 50)
        { returned = Chest.Diamond; }
        else if (chance <= 95 && chance > 80)
        { returned = Chest.Rainbow; }
        else
        { returned = Chest.Glitch; }
        Debug.Log("Шанс стал на отметке " + chance + "%, выпал " + returned.ToString() + " Chest!");
        return returned;
    }
    public void SpawnChest()
    {
        Spawner.transform.position = new Vector3(Random.Range(-8.0f, 8.0f), Spawner.transform.position.y, Spawner.transform.position.z);
        switch (RandomiseChest())
        {
            case Chest.Diamond:
                ClonedChest = Instantiate(ChestPrefabs[1], Spawner.transform.position, Quaternion.identity) as GameObject;
                Debug.Log("Мы все умрем");
                break;
            case Chest.Rainbow:
                ClonedChest = Instantiate(ChestPrefabs[2], Spawner.transform.position, Quaternion.identity) as GameObject;
                Debug.Log("Мы все умрем");
                break;
            case Chest.Glitch:
                ClonedChest = Instantiate(ChestPrefabs[3], Spawner.transform.position, Quaternion.identity) as GameObject;
                Debug.Log("Мы все умрем");
                break;
            default: // Chest.Classic
                ClonedChest = Instantiate(ChestPrefabs[0], Spawner.transform.position, Quaternion.identity) as GameObject;
                Debug.Log("Мы все умрем");
                break;
        }
    }
}
