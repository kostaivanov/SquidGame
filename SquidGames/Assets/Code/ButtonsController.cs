using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private OnClickMove[] redButtons, blueButtons, greenButtons, whiteButtons;
    [SerializeField] internal Sprite[] numbersImages;
    private GameObject[] players;
    private PlayerHealth playerHealthBlue, PlayerHealthRed;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name.StartsWith("B"))
            {
                playerHealthBlue = players[i].GetComponent<PlayerHealth>();
            }
            else
            {
                PlayerHealthRed = players[i].GetComponent<PlayerHealth>();

            }
        }
    }

    private void Update()
    {
        if (playerHealthBlue != null && playerHealthBlue.dead == true && playerHealthBlue.numbersChanged == false)
        {
            for (int i = 0; i < redButtons.Length; i++)
            {
                Image image = blueButtons[i].GetComponent<Image>();
                //blueButtons[i].moveNumber.text = UnityEngine.Random.Range(1, 5).ToString();
                image.sprite = numbersImages[Random.Range(0, numbersImages.Length)];
                playerHealthBlue.numbersChanged = true;

                //Debug.Log("asdadadasdas");
            }
        }
        if (PlayerHealthRed != null && PlayerHealthRed.dead == true && PlayerHealthRed.numbersChanged == false)
        {
            for (int i = 0; i < redButtons.Length; i++)
            {
                Image image = redButtons[i].GetComponent<Image>();

                //redButtons[i].moveNumber.text = UnityEngine.Random.Range(1, 5).ToString();
                image.sprite = numbersImages[Random.Range(0, numbersImages.Length)];

                PlayerHealthRed.numbersChanged = true;

                //Debug.Log("asdadadasdas");
            }
        }
    }
}
