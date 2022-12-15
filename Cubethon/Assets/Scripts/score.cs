using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text scoreText;
    public static score instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetScore()
    {
        scoreText.text = "Checkpoints Completed"+ PlayerMvmt.instance.score.ToString("0");
    }
}
