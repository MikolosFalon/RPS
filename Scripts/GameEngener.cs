using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameEngener : MonoBehaviour
{
    //collection class
    [System.Serializable]
    public class DAB
    {
        public Sprite dick;
        public Sprite ass;
        public Sprite breast;
    }
    public List<DAB> dab;

    [ Header( "Player Cards")]
    public List<Image> playerCards;
    [ Header ("Enemy Cards")]
    public List<Image> EnemyCards;

    [Header("active button")]
    public Image SelectCard;
    [Header("Enemy button")]
    public Image SelectEnemyCard;
    [Header("Defoult image")]
    public Sprite defoiltImage;

    //text win
    [Header("resultat")]
    public TMP_Text resuult;

    //power player card
    private int powerCard;
    //power enemy card
    private int selectRandomCard;
    //match end 
    private bool matchEnd = false;
    //score
    private int score=0;
    private int hiscore=0;
    [Header("filds for score")]
    public TMP_Text scoreText;
    public TMP_Text hiScoreText;

    private void Start()
    {
        StartGame();
        if (PlayerPrefs.GetInt("Hiscore") !=0)
        {
            hiscore = PlayerPrefs.GetInt("Hiscore");
        }
        else
        {
            hiscore = 0;
        }
        hiScoreText.text = hiscore.ToString();
        scoreText.text = "0";
    }
    public void StartGame()
    {
        matchEnd = false;
        resuult.text = "";

        // select random nomber for cart
        int selectNomberPlayer = Random.Range(0, dab.Count);
            int selectNomberEnemy = Random.Range(0, dab.Count);

            //set sprites
            playerCards[0].sprite = dab[selectNomberPlayer].dick;
            playerCards[1].sprite = dab[selectNomberPlayer].ass;
            playerCards[2].sprite = dab[selectNomberPlayer].breast;

            EnemyCards[0].sprite = dab[selectNomberEnemy].dick;
            EnemyCards[1].sprite = dab[selectNomberEnemy].ass;
            EnemyCards[2].sprite = dab[selectNomberEnemy].breast;
            //set select cards fild
            SelectCard.sprite = defoiltImage;
            SelectEnemyCard.sprite = defoiltImage;


    }
    //buttom player
    public void DickButtom()
    {
        if (!matchEnd) { 
        SelectCard.sprite = playerCards[0].sprite;
        powerCard = 0;
        matchEnd = true;
        StartCoroutine(EnemyStep());
    }
    }
    public void AssButtom()
    {
        if (!matchEnd)
        {
            SelectCard.sprite = playerCards[1].sprite;
            powerCard = 1;
            matchEnd = true;
            StartCoroutine(EnemyStep());
        }
    }
    public void BruButtom()
    {
        if (!matchEnd)
        {
            SelectCard.sprite = playerCards[2].sprite;
            powerCard = 2;
            matchEnd = true;
            StartCoroutine(EnemyStep());
        }
    }
    //enemy step
    IEnumerator EnemyStep()
    {
        
        selectRandomCard = Random.Range(0, EnemyCards.Count);
        yield return new WaitForSeconds(2.0f);
        SelectEnemyCard.sprite = EnemyCards[selectRandomCard].sprite;
        MachEnd();
        yield return null;
    }
    //resultat
    void MachEnd()
    {
        Debug.Log("MachEnd");
        Debug.Log("powerCard "+powerCard);
        Debug.Log("selectRandomCard " + selectRandomCard);

        matchEnd = true;
        //draw
        if (powerCard == selectRandomCard)
        {
            resuult.text = "Draw";
        }
        else
        {
            if (powerCard == 0)
            {
                power0();

            }
            if (powerCard == 1)
            {
                power1();

            }
            if (powerCard == 2)
            {
                power2();

            }
        }
    }
    void power0()
    {        if (selectRandomCard == 1)
            {
                //win
                resuult.text = "Win";
                score++;
                FinalScore();
            }
            if (selectRandomCard == 2)
            {
                //fail
                resuult.text = "Fail";
                score=0;
            FinalScore();
        }

    }
    void power1()
    {      
        if (selectRandomCard == 0)
            {
                //fail
                resuult.text = "Fail";
                score=0;
            FinalScore();
        }

        if (selectRandomCard == 2)
            {
                //win
                resuult.text = "Win";
                score++;
            FinalScore();
        }


    }
    void power2()
    {
        if (selectRandomCard == 0)
            {
                //win
                resuult.text = "Win";
                score++;
            FinalScore();
        }
        if (selectRandomCard == 1)
            {
                //fail
                resuult.text = "Fail";
                score=0;
            FinalScore();
        }

    }
    //final score 
    void FinalScore()
    {
        if (score> hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetInt("Hiscore", hiscore);

        }
        hiScoreText.text = hiscore.ToString();
        scoreText.text = score.ToString();
    }

}
