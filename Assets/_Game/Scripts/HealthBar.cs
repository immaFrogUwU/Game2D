using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;   //do lech giua UI va orivate

    float hp;
    float maxHp;
    private Transform target;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        //Lerp 1 hàm tuyến tính tính lượng máu còn lại trong 1 time
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp/maxHp, Time.fixedDeltaTime *5f);
        transform.position = target.position + offset; 
    }

    // Update is called once per frame
    public void OnInit(float maxHp, Transform target)
    {
        this.target = target;
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1; 
    }
    public void SetNewHp(float hp)
    {
        this.hp = hp;
        //imageFill.fillAmount = hp/maxHp; thay đổi máu
    }
}
