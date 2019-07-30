using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("ButtonHandler");
        BlockChainController bcc = BlockChainController.getInstance();
        bcc.PayButton_OnClick();
    }
}
