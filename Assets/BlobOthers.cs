using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlobOthers : MonoBehaviour
{
    #region TRAP VIEW
    [SerializeField] Animator trapImage;

    public void SetTrapView(bool _p)
    {
        trapImage.SetBool("Cool", _p);
    }
    #endregion
    [Header("Dash:")]
    [SerializeField] int maxDashAmount = 3;
    [SerializeField] float cooldown = 2f;
    [Space]
    [SerializeField] Image cooldownImg;
    [SerializeField] TMP_Text amountInfo;
    [SerializeField] float pwr = 100;
    [SerializeField][Range(0,1)] float arc;
    int dashAmount = 3;
    bool recharge = false;

    private void Start()
    {
        dashAmount = maxDashAmount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashAmount > 0)
        {
            GetComponent<Rigidbody>().AddForce((transform.forward + (Vector3.up * arc)) * 10 * pwr);
            dashAmount--;
        }
        if (dashAmount < maxDashAmount)
        {
            if (!recharge) { StartCoroutine(Recharge()); }
        }
        amountInfo.text = $"{dashAmount} / {maxDashAmount}";
    }

    IEnumerator Recharge() 
    {
        recharge = true;
        float tick = .1f;
        WaitForSeconds wait = new WaitForSeconds(tick);
        int tickAmount = (int)(cooldown / tick);
        for (int i = 0; i < tickAmount; i++)
        {
            float percent = Mathf.Clamp(tick * i / cooldown, 0, 1);
            cooldownImg.fillAmount = percent;
            yield return wait;
        }
        cooldownImg.fillAmount = 1;
        dashAmount++;
        if(dashAmount < maxDashAmount) { StartCoroutine(Recharge()); }
        else { recharge = false; }
    }
}