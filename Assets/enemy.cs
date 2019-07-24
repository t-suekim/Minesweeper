using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Sprite defaultTexture;
    public Sprite affectedTexture;

    // Start is called before the first frame update
    void Start()
    {
        PlayField.enemyStatus = default;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayField.status.Equals("Default"))
        {
            GetComponent<SpriteRenderer>().sprite = defaultTexture;

        }
        else if (PlayField.status.Equals("Affected"))
        {
            GetComponent<SpriteRenderer>().sprite = affectedTexture;
        }
    }
}
