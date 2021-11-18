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
    [SerializeField] new MeshRenderer renderer;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;

    Animator animator;

    [SerializeField] PhotonView PV;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
    }

    public override void Use()
    {
        Shoot();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && PV.IsMine)
        {
            animator.SetTrigger("swap");
        }
    }
    void ChangeMode()
    {
        PV.RPC("RPC_ChangeMode", RpcTarget.All);
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
        }
        else
        {
            shootPoint.localEulerAngles = Vector3.zero;
            Destroy(Instantiate(bullet, shootPoint.position, shootPoint.rotation), 1f);
        }
    }
}