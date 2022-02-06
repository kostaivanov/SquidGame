using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour, IDestroyable
{
    private GameObject[] players;
    [SerializeField] private List<Button> moveButtons;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
 
    private void OnEnable()
    {
        BombController.OnBombExplodeHandler += Deactivate;
        BombController.OnBombExplodeHandler += Restart;
    }

    private void OnDisable()
    {
        BombController.OnBombExplodeHandler -= Deactivate;
        BombController.OnBombExplodeHandler -= Restart;
 
    }

    public void Deactivate(GameObject bombObject, GameObject playerObject, Vector3 playerStartPosition)
    {
        bombObject.GetComponent<SpriteRenderer>().enabled = false;
        foreach (Transform bodyPart in playerObject.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Restart(GameObject bombObject, GameObject playerObject, Vector3 playerStartPosition)
    {
        playerObject.transform.position = playerStartPosition;
        PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
        MovePlayer movePlayer = playerObject.GetComponent<MovePlayer>();

        foreach (Transform bodyPart in playerObject.transform)
        {
            bodyPart.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        foreach (Button button in moveButtons)
        {
            button.interactable = true;
        }

        if (movePlayer != null)
        {
            movePlayer.currentIndex = -1;
            movePlayer.initialIndex = movePlayer.currentIndex;
        }
        
        if (playerHealth != null)
        {
            playerHealth.dead = false;
            playerHealth.numbersChanged = false;
        }

        playerObject.transform.localScale = new Vector2(-0.5f, 0.5f);
        movePlayer.rotationChanged = false;
        playerHealth.dead = true;
    }
}
