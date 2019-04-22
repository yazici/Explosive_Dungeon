using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicChest : MonoBehaviour
{
    public static ClassicChest Instance;
    public GameObject[] ChestPrefabs; // Префабы сундуков
    public Transform Spawner;
    public static Coroutine DiamondCoroutine, InvisibleCoroutine, SpeedBoostCoroutine;
    public static GameObject ClonedChest; // 
    private void Start()
    {
        Instance = this;
        InvokeRepeating("SpawnChest", 4f, 20f);
    }
    public enum Chest { Classic, Diamond, Rainbow, Glitch }
    public Chest RandomiseChest() // Classic - 50%; Diamond - 30%; Rainbow - 15%; Glitch - 5%;
    {
        int chance = Random.Range(0, 101);
        Chest returned;
        if (chance <= 60)
        { returned = Chest.Classic; }
        else if (chance <= 80 && chance > 60)
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
                Instantiate(ChestPrefabs[1], Spawner.transform.position, Quaternion.identity);
                Debug.Log("Алмазный сундук заспавнен!");
                break;
            case Chest.Rainbow:
                Instantiate(ChestPrefabs[2], Spawner.transform.position, Quaternion.identity);
                Debug.Log("Радужный сундук заспавнен!");
                break;
            case Chest.Glitch:
                Instantiate(ChestPrefabs[3], Spawner.transform.position, Quaternion.identity);
                Debug.Log("Глитч-сундук заспавнен!");
                break;
            default: // Chest.Classic
                Instantiate(ChestPrefabs[0], Spawner.transform.position, Quaternion.identity);
                Debug.Log("Классический сундук заспавнен!");
                break;
        }
    }
}
