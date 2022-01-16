using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
internal class InstantiateItems : MonoBehaviour
{
    [SerializeField] private GameObject[] boxes;
    [SerializeField] private GameObject[] items;
    private List<int> usedIndexes;
    private List<int> plusesMinuses;
    private List<int> pushForward;
    private List<int> pushBack;

    private bool spawned;

    // Start is called before the first frame update
    void Start()
    {
        usedIndexes = new List<int>();
        plusesMinuses = new List<int>();
        pushForward = new List<int>();
        pushBack = new List<int>();
        spawned = false;
    }

    private void SpawnRandomBombs()
    {
        int index = Random.Range(0, boxes.Length -1);

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
            GameObject obj = Instantiate(items[0], boxes[index].transform.position, boxes[index].transform.rotation, boxes[index].transform);

            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void SpawnPlusAndMinus()
    {
        int index = Random.Range(0, boxes.Length - 1);

        if (!usedIndexes.Contains(index) && !plusesMinuses.Contains(index))
        {
            plusesMinuses.Add(index);
            usedIndexes.Add(index);
            GameObject obj = Instantiate(items[Random.Range(1, 3)], boxes[index].transform.position, boxes[index].transform.rotation, boxes[index].transform);

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
            while (plusesMinuses.Count < 8)
            {
                SpawnPlusAndMinus();
            }
            while (pushForward.Count < 2)
            {
                SpawnPushForwardObject();
            }
            while (pushBack.Count < 2)
            {
                SpawnPushBackObject();
            }
            spawned = true;
        }
    }

    internal static void Shuffle(GameObject[] platforms, GameObject collectable)
    {
        bool indexFound = false;
        do
        {
            // Find a random index
            int destIndex = Random.Range(0, platforms.Length);
            GameObject source = collectable.transform.parent.gameObject;
            GameObject destination = platforms[destIndex];

            // If is not identical
            if (source != destination && destination.transform.childCount == 0)
            {

                // Swap the position
                collectable.transform.position = destination.transform.position;

                collectable.transform.parent = platforms[destIndex].transform;
                indexFound = true;
                // Swap the array item
            }
        } 
        while (indexFound == false);       
    }

    internal static void SpawnRandomObject(GameObject[] collectables, GameObject currentCollectable)
    {

        GameObject spawnObj = Instantiate(collectables[Random.Range(0, collectables.Length)], currentCollectable.transform.position, currentCollectable.transform.rotation, currentCollectable.transform.parent);
        SpriteRenderer sprite = spawnObj.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.enabled = false;
            spawnObj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    private void SpawnPushForwardObject()
    {
        int index = Random.Range(0, boxes.Length - 3);

        if (!usedIndexes.Contains(index) && !pushForward.Contains(index))
        {
            pushForward.Add(index);
            usedIndexes.Add(index);
            GameObject obj = Instantiate(items[3], boxes[index].transform.position, items[3].transform.rotation, boxes[index].transform);
            if (index > 9)
            {
                obj.transform.localScale = new Vector2(-1, 1);
            }
            obj.GetComponent<SpriteRenderer>().enabled = false;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }      
    }

    private void SpawnPushBackObject()
    {
        int index = Random.Range(2, boxes.Length - 1);

        if (!usedIndexes.Contains(index) && !pushBack.Contains(index))
        {
            pushBack.Add(index);
            usedIndexes.Add(index);
            GameObject obj = Instantiate(items[4], boxes[index].transform.position, items[4].transform.rotation, boxes[index].transform);
            if (index > 9)
            {
                obj.transform.localScale = new Vector2(-1, 1);
            }
            obj.GetComponent<SpriteRenderer>().enabled = false;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
