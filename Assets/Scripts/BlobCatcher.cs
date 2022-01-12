using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class BlobCatcher : Gun
{
    [SerializeField] Camera cam;
    bool mode = true;
    [SerializeField] Material firstMat;
    [SerializeField] Material secondMat;

    [SerializeField] Material firstCol;
    [SerializeField] Material secondCol;

    [SerializeField] Sprite firstSpr;
    [SerializeField] Sprite secondSpr;

    [SerializeField] GameObject firstBul;
    [SerializeField] GameObject secondBul;

    [SerializeField] Image HUD_img;
    [SerializeField] Image HUD_bg;
    [SerializeField] Text HUD_txt;
    [SerializeField] Image HUD_bar;

    [SerializeField] GameObject HUD_gun1;
    [SerializeField] GameObject HUD_gun2;

    [SerializeField] new MeshRenderer renderer;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;

    Animator animator;

    [SerializeField] PhotonView PV;

    [Space]

    [SerializeField] int maxAmmo1 = 5;
    int curAmmo1;
    [SerializeField] float cooldown1 = 3;
    float waitTime1;

    [Space]

    [SerializeField] int maxAmmo2 = 3;
    int curAmmo2;
    [SerializeField] float cooldown2 = 3;
    float waitTime2;

    bool rech1, rech2;

    const float tickTime = .01f;
    WaitForSeconds tick = new WaitForSeconds(tickTime);

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        curAmmo1 = maxAmmo1;
        curAmmo2 = maxAmmo2;
    }

    public override void Use()
    {
        if (mode && curAmmo1 > 0)
        {
            Shoot();
            curAmmo1--;
            if (!rech1)
            {
                StartCoroutine(Recharge1());
            }
        }
        else if(!mode && curAmmo2 > 0)
        {
            Shoot();
            curAmmo2--;
            if (!rech2)
            {
                StartCoroutine(Recharge2());
            }
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && PV.IsMine)
        {
            animator.SetTrigger("swap");
        }
        if (mode)
        {
            HUD_txt.text = curAmmo1 + " / " + maxAmmo1;
            HUD_bar.fillAmount = waitTime1 / cooldown1;
        }
        else if (!mode)
        {
            HUD_txt.text = curAmmo2 + " / " + maxAmmo2;
            HUD_bar.fillAmount = waitTime2 / cooldown2;
        }
    }
    void ChangeMode()
    {
        PV.RPC("RPC_ChangeMode", RpcTarget.All);
        itemUI.SetActive(false);
        if (mode) { itemUI = HUD_gun1; }
        else if (!mode) { itemUI = HUD_gun2; }
        itemUI.SetActive(true);
    }
    [PunRPC]
    void RPC_ChangeMode()
    {
        mode = !mode;
        if (mode)
        {
            Material[] materials = renderer.materials;
            materials[1] = firstMat;
            materials[3] = firstMat;
            renderer.materials = materials;
            HUD_img.sprite = firstSpr;
            HUD_bg.material = firstCol;
            bullet = firstBul;
        }
        else
        {
            Material[] materials = renderer.materials;
            materials[1] = secondMat;
            materials[3] = secondMat;
            renderer.materials = materials;
            HUD_img.sprite = secondSpr;
            HUD_bg.material = secondCol;
            bullet = secondBul;
        }
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            //hit.collider.gameObject.GetComponent<ICatchable>()?.Catch(true);
            shootPoint.LookAt(hit.point);
            Destroy(Instantiate(bullet, shootPoint.position, shootPoint.rotation), 2f);
            //Destroy(Instantiate(bullet, cam.transform.position, shootPoint.rotation), 2f);
        }
        else
        {
            shootPoint.localEulerAngles = Vector3.zero;
            Destroy(Instantiate(bullet, shootPoint.position, shootPoint.rotation), 1f);
        }
    }

    IEnumerator Recharge1()
    {
        rech1 = true;
        if (curAmmo1 < maxAmmo1)
        {
            waitTime1 = 0;
            while (waitTime1 < cooldown1)
            {
                yield return tick;
                waitTime1 += tickTime;
            }
            curAmmo1++; 
        }
        if (curAmmo1 < maxAmmo1)
        {
            StartCoroutine(Recharge1());
        }
        else
        {
            rech1 = false;
            waitTime1 = 0;
        }
    }

    IEnumerator Recharge2()
    {
        rech2 = true;
        if (curAmmo2 < maxAmmo2)
        {
            waitTime2 = 0;
            while (waitTime2 < cooldown2)
            {
                yield return tick;
                waitTime2 += tickTime;
            }
            curAmmo2++;
        }
        if (curAmmo2 < maxAmmo2)
        {
            StartCoroutine(Recharge2());
        }
        else 
        { 
            rech2 = false;
            waitTime2 = 0;
        }
    }
}