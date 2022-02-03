using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Tasks : MonoBehaviour
{
    [SerializeField] List<Task> availableTasks;
    [SerializeField] int maxTasks = 3;
    public List<Task> activeTasks;
    [Space]
    [SerializeField] TMP_Text completionInfo;

    private void Start()
    {
        SelectTasks();
    }

    private void Update()
    {
        completionInfo.text = HowManyActive() + " / " + maxTasks;
        if (HowManyActive() == 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    void SelectTasks()
    {
        for (int i = 0; i < maxTasks; i++)
        {
            int index = Random.Range(0, availableTasks.Count);
            Task task = availableTasks[index];
            activeTasks.Add(task);
            availableTasks.Remove(task);
        }
        foreach (Task t in activeTasks)
        {
            if((int)PhotonNetwork.LocalPlayer.CustomProperties["RoleID"] != 1) { t.Init(); }
        }
    }

    int HowManyActive()
    {
        int c = 0;
        for (int i = 0; i < activeTasks.Count; i++)
        {
            if (activeTasks[i].active) { c++; }
        }
        return c;
    }
}