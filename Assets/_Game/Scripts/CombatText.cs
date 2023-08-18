using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    //Init va Despawn la 2 ham tu dinh nghia
    [SerializeField] Text hpText; 
    public void OnInit(float damage)
    {
        //hp dang la float va chung ta muon gan text vao thi dung ToString() de thay doi
        hpText.text = damage.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
