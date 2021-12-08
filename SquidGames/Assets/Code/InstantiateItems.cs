using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class InstantiateItems : MonoBehaviour
{
    [SerializeField] private GameObject[] boxes;
    [SerializeField] private GameObject[] items;
    private List<int> usedIndexes;
    private List<int> plusesMinuses;
    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        usedIndexes = new List<int>();
        plusesMinuses = new List<int>();
        spawned = false;
    }

    private void SpawnRandomBombs()
    {
        int index = Random.Range(0, boxes.Length - 1);

        if (!usedIndexes.Contains(index))
        {
            if ((index - 1 >= 0 && usedIndexes.Contains(index - 1)) && (index - 2 >= 0 && usedIndexes.Contains(index - 2)))
            {
                return;
            }
            if ((index + 1 <= boxes.Length - 1 && usedIndexes.Contains(index + 1)) && (index + 2 <= boxes.Length - 1 && usedIndexes.Contains(index + 2)))
            {
                return;
            }
            if ((index - 1 >= 0 && usedIndexes.Contains(index - 1)) && (index + 1 <= boxes.Length - 1 && usedIndexes.Contains(index + 1)))
            {
                return;
            }
            usedIndexes.Add(index);
            GameObject obj = Instantiate(items[0], boxes[index].transform.position, boxes[index].transform.rotation);

            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void SpawnPlusAndMinus()
    {
        int index = Random.Range(0, boxes.Length - 1);

        if (!usedIndexes.Contains(index) && !plusesMinuses.Contains(index))
        {
            plusesMinuses.Add(index);
            GameObject obj = Instantiate(items[Random.Range(1, 3)], boxes[index].transform.position, boxes[index].transform.rotation);

            obj.GetComponent<SpriteRenderer>().enabled = false;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned == false)
        {
            while (usedIndexes.Count < 4)
            {
                SpawnRandomBombs();
            }
            while (plusesMinuses.Count < 4)
            {
                SpawnPlusAndMinus();
            }
            spawned = true;
        }
    }
}
