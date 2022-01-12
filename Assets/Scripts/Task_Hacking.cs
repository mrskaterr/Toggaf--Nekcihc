using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Hacking : TaskLogic
{
    [SerializeField] GameObject hackPanel;
    GameObject player;

    public override void AllUHave2Do(GameObject _player)
    {
        if (task.active)
        {
            player = _player;
            hackPanel.SetActive(true);
            _player.GetComponent<PlayerController>().SetMovement(false);
            _player.GetComponent<UIContainer>().ToggleCrosshair(false);
        }
    }

    private void Update()
    {
        if (hackPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Q)) 
        {
            player.GetComponent<PlayerController>().SetMovement(true);
            player.GetComponent<UIContainer>().ToggleCrosshair(true);
            hackPanel.SetActive(false);
        }
    }
}