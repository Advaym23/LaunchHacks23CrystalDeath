using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBoardScript : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    int score = 0;
    int health = 0;

    // Start is called before the first frame update
    private void Start()
    {
        //scoreText.text = score.ToString();
        //healthText.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
