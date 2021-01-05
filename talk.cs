using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class talk : MonoBehaviour
{

    [SerializeField] RectTransform prefab = null;                                          //トーク表示テキスト
    [SerializeField] public Text yourName;                                                 //トーク相手名表示テキスト
    [SerializeField] public InputField talkText;                                           //トーク入力インプットフィールド

    IEnumerator Start()
    {
        if (solutionClass.userName != null)
        {
            yourName.text = solutionClass.userName;
        }
        else if (goTalkButton.userName != null)
        {
            yourName.text = goTalkButton.userName;
        }

        yield return StartCoroutine(getTalk());

        StartCoroutine(result());
    }

    private IEnumerator getTalk()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("myId", login.userId);

        if (solutionClass.yourId != null)
        {
            dic.Add("yourId", solutionClass.yourId);
        }
        else if (goTalkButton.yourId != null)
        {
            dic.Add("yourId", goTalkButton.yourId);
        }

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/getTalk.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        var talks = JsonUtility.FromJson<talkClasss>(DbProcess.returnText);


        foreach (var talk in talks.talk)
        {
            var item = GameObject.Instantiate(prefab) as RectTransform;
            item.SetParent(transform, true);

            var text = item.GetComponentInChildren<Text>();

            if (talk.meTalk != null)
            {
                Debug.Log(talk.meTalk);
                text.color = Color.red;
                text.text = talk.meTalk;
            }
            else if (talk.yourTalk != null)
            {
                Debug.Log(talk.yourTalk);
                text.color = Color.blue;
                text.text = talk.yourTalk;
            }

        }

        yield return null;
    }

    public void talkPostFunc()
    {
        if (talkText.text != "")
        {

            StartCoroutine(talkPost());

            SceneManager.LoadScene("talk");
        }

    }

    private IEnumerator talkPost()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("myId", login.userId);

        if (solutionClass.yourId != null)
        {
            dic.Add("you_id", solutionClass.yourId);
        }
        else if (goTalkButton.yourId != null)
        {
            dic.Add("you_id", goTalkButton.yourId);
        }

        dic.Add("talk", talkText.text);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/talkPost.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));

    }


    public void talkListScene()
    {
        SceneManager.LoadScene("talkList");
    }

    public void solutionListScene()
    {
        SceneManager.LoadScene("solutionList");
    }
}

[Serializable]

public class talkClass
{
    public string meTalk;
    public string yourTalk;
}

[Serializable]

public class talkClasss
{
    public List<talkClass> talk;
}