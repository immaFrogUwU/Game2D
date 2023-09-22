using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayAtk : MonoBehaviour
{
    public Button buttonClick;
    public float delayTime;

    private void Start()
    {
        buttonClick.onClick.AddListener(SetAttackTime);   
    }

    public void SetAttackTime()
    {
        StartCoroutine(WaitToAttack()); 
    }

    IEnumerator WaitToAttack()
    {
        buttonClick.interactable = false;
        yield return new WaitForSeconds(delayTime);
        buttonClick.interactable = true;
    }
}
