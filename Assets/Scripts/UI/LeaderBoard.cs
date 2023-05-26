using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using Dan.Models;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;

    private string publicLeaderBoardKey =
        "e2a3c7843496a5d2ad9f58c6478f721f5188d4b7e0a23d97dd28dcbdc1bab05e";

    private void Start()
    {
        Load();
    }

    public void Load()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey, OnLeaderboardLoaded);
    }

    private void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (var entryField in names)
        {
            entryField.text = "Name";
        }

        foreach (var entryFieldScore in scores)
        {
            entryFieldScore.text = "Score";
        }

        for (int i = 0; i < entries.Length; i++)
        {
            names[i].text = entries[i].Username;
            scores[i].text = entries[i].Score.ToString();
        }
    }

    public void Submit()
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey, inputName.text, int.Parse(inputScore.text), Callback);
    }

    void Callback(bool success)
    {
        if(success)
        {
            Load();
        }
    }
}
