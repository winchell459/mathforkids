using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{
    public static int level = 1;

    [SerializeField] GameObject tilePrefab, pickupPrefab;
    [SerializeField] Transform tileHolder;
    [SerializeField] GameOverPrompt gameOverPrompt;
    [SerializeField] NumberPad numPad;

    [SerializeField] Ball ball;
    [SerializeField] Text scoreText;
    int score = 0;
    int maxScore;

    float currentXPos;
    [SerializeField]List<int> numbers;

    float yStart;
    float finishXPos = Mathf.Infinity;

    [SerializeField] bool wallHeight = false;


    // Start is called before the first frame update
    void Start()
    {
        
        yStart = tilePrefab.transform.position.y;
        currentXPos = tilePrefab.transform.position.x + 1;

        InitializeLevelParameters();

        maxScore = numbers.Count+1;
        numPad.SetupNumPad(numbers);

        PopRandomHeight(wallHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(numbers.Count > 0 && ball.transform.position.x > currentXPos - 2)
        {
            PopRandomHeight(wallHeight);
        }else if(ball.transform.position.x >= finishXPos)
        {
            HandleGameOver(true);
        }
    }

    private void InitializeLevelParameters()
    {
        if(level == 1)
        {
            wallHeight = false;
        }
        else if(level == 2)
        {
            wallHeight = false;
        }
        else
        {
            wallHeight = true;
        }
    }

    private void PopRandomHeight(bool wallHeight)
    {
        int height = numbers[Random.Range(0, numbers.Count)];
        numbers.Remove(height);
        if (level > 2)
        {
            SetNextTileSection(height + 1, 2,1);
        }
        else if(level == 1)
        {
            SetNextTileSection(1, 2, height + 1);
        }
        else if (level == 2)
        {
            SetNextTileSection(Mathf.Max(height/2, 1), 2, height + 1);
        }

    }

    private void SetNextTileSection(int height, int gap, int pickupYOffset)
    {
        SetTileGap(gap, pickupYOffset);
        SetTileHeight(height, currentXPos);
        if (numbers.Count == 0)
        {
            SetTileGap(gap, 1);
            SetEndTile();
        }
        
    }

    private void SetTileGap(int gap, int pickupYOffset)
    {
        for(int i = 0; i < gap; i++)
        {
            SetTileHeight(1, currentXPos);
        }
        SetPickup(currentXPos-1, pickupYOffset);
    }

    private void SetPickup(float xPos, int yOffset)
    {
        Pickup pickup = Instantiate(pickupPrefab, new Vector2(xPos, yStart + yOffset), Quaternion.identity).GetComponent<Pickup>();
        if (yOffset > 1) pickup.SetupPickupText((yOffset - 1).ToString());
        else pickup.SetupPickupText("");
    }

    private void SetTileHeight(int height, float xPos)
    {
        
        for(int i =0; i < height; i++)
        {
            Vector2 pos = new Vector2(xPos, yStart + i);
            //Color color = (i + (int)xPos) % 2 == 0 ? Color.black : Color.gray;
            Color color = Color.white;
            PlaceTile(pos, color, i==0 ,height);
        }
        currentXPos++;
    }

    private void SetEndTile()
    {
        Vector2 pos = new Vector2(currentXPos, yStart);
        PlaceTile(pos, Color.red, true, 1);
        finishXPos = currentXPos;
    }

    private void PlaceTile(Vector2 pos, Color color, bool hasCollider, int height)
    {
        SpriteRenderer sprite = Instantiate(tilePrefab, pos, Quaternion.identity, tileHolder).GetComponent<SpriteRenderer>();
        sprite.color = color;
        if (hasCollider)
        {
            //BoxCollider2D box = sprite.gameObject.AddComponent<BoxCollider2D>();
            BoxCollider2D box = sprite.GetComponent<BoxCollider2D>();

            box.offset = new Vector2(0, (box.size.y * height - 1) / 2);
            box.size = new Vector2(box.size.x, box.size.y * height);

        }
        else
        {
            sprite.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void NumberPadInput(int num, int numCount)
    {
        ball.Jump(num);
        ball.MovesComplete = numCount <= 0;
    }

    public void BallStuck()
    {
        HandleGameOver(false);
    }

    public void Pickup()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void HandleGameOver(bool win)
    {
        numPad.gameObject.SetActive(false);
        ball.Stop = true;
        int finalScore = 0;
        if (win)
        {
            if (maxScore - score == 0) finalScore = 3;
            else if (maxScore - score == 1 && maxScore > 1) finalScore = 2;
            else if (score > 1) finalScore = 1;

            PlayerSave.SaveLevel(level, finalScore);
        }
        
        gameOverPrompt.Display(finalScore);

    }
}
