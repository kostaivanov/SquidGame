using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeItems : MonoBehaviour
{
    [SerializeField] private GameObject[] boxes;
    [SerializeField] private GameObject[] items;
    private List<int> usedIndexes;
    private bool spawned;
    private int index = 4;

    // Start is called before the first frame update
    void Start()
    {
        usedIndexes = new List<int>();
        spawned = false;
    }
    private void SpawnRandom()
    {
        int index = Random.Range(0, boxes.Length);

        if (!usedIndexes.Contains(index))
        {
            usedIndexes.Add(index);
            GameObject obj = Instantiate(items[0], boxes[index].transform.position, boxes[index].transform.rotation);
            index--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned == false)
        {
            Debug.Log("123");
            while (index > 0)
            {
                SpawnRandom();
            }
            spawned = true;
            //index = 0;
        }
    }
}
