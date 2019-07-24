using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        PlayField.resetBoard += PlayField.w * PlayField.h;
    }
}
