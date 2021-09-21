using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedGreenMushroom : GreenMushroom
{
    //Cogumelo verdes que são spawn de caixas são destruido com a função reset item.
    protected override void OnEnable()
    {
        GameManager.Instance.onResetItems += ResetItem;
    }

    protected override void ResetItem()
    {
        GameManager.Instance.onResetItems -= ResetItem;
        Destroy(gameObject);
    }
}
