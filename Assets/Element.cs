using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour
{

    // Is this a mine?
    public bool mine;

    // Is it currently flagged?
    public bool flag;

    // Different Textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    public Sprite flagTexture;
    public Sprite defaultTexture;
    


    // Is it still covered?
    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    
    // Load another texture
    public void loadTexture(int adjacentCount)
    {
        if (mine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else if (flag)
            GetComponent<SpriteRenderer>().sprite = flagTexture;
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
    }

    void OnMouseUpAsButton()
    {
        
    }

    void OnMouseOver()
    {
        if (!PlayField.gameOver)
        {
            if (Input.GetMouseButtonDown(0) && GetComponent<SpriteRenderer>().sprite.texture.name == "default")
            {
                // It's a mine
                if (mine)
                {
                    PlayField.status = "Boom!";
                    // uncover all mines
                    PlayField.uncoverMines();

                    // game over
                    PlayField.gameOver = true;
                    print("you lose");

                }
                // It's not a mine
                else
                {
                    PlayField.status = "Empty";
                    // show adjacent mine number
                    int x = (int)transform.position.x;
                    int y = (int)transform.position.y;
                    loadTexture(PlayField.adjacentMines(x, y));

                    // TODO: uncover area without mines
                    PlayField.FFuncover(x, y, new bool[PlayField.w, PlayField.h]);

                    // find out if the game was won now
                    if (PlayField.isFinished() && !(PlayField.isOpened))
                    {
                        print("you win");
                        PlayField.score += 1;
                        PlayField.status = "Clear!";
                        PlayField.isOpened = true;

                    }
                }
            }
            else if (Input.GetMouseButtonDown(0) && GetComponent<SpriteRenderer>().sprite.texture.name == "flag")
            {
                // Do nothing
            }
            else
            {
                // Logic for left clicking number
            }
            if (Input.GetMouseButtonDown(1))
            {
                ToggleFlag();
            }
        }
    }

    private void ToggleFlag()
    {
        // If flagged, unflag
        if (GetComponent<SpriteRenderer>().sprite.texture.name == "flag")
        {
            GetComponent<SpriteRenderer>().sprite = defaultTexture;
            flag = false;
        }  // If unflagged, flag
        else if (GetComponent<SpriteRenderer>().sprite.texture.name == "default")
        {
            GetComponent<SpriteRenderer>().sprite = flagTexture;
            flag = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        // Randomly decide if it's a mine or not
        mine = Random.value < PlayField.density;

        // Register in Grid
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        PlayField.elements[x, y] = this;
    }

    private void Update()
    {
        print("Called update");
        if (PlayField.resetBoard > 0)
        {
            PlayField.isOpened = false;
            ReRoll();
            PlayField.resetBoard--;
        }
    }
    public void ReRoll()
    {
        print("Called a reroll");
        // Randomly decide if it's a mine or not
        mine = Random.value < PlayField.density;

        // Register in Grid
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        PlayField.elements[x, y] = this;

        GetComponent<SpriteRenderer>().sprite = defaultTexture;
    }

}