using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text coinText;
    //neu dung Awake thi phai public
    public static UIManager instance;

    //SINGLETON//

    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindAnyObjectByType<UIManager>();
    //        }
    //        return instance;
    //    }
    //}
    private void Awake()
    {
        instance = this; 
    }
    public void SetCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
