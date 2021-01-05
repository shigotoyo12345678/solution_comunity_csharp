using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class talkList : MonoBehaviour
{

    [SerializeField] RectTransform prefab = null;     //トークボタン

    IEnumerator Start()
    {
        solutionClass.userName = null;
        solutionClass.yourId = null;

        yield return StartCoroutine(getTalkList());

        StartCoroutine(result());
    }

    private IEnumerator getTalkList()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("me_id", login.userId);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/getTalkCatalog.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        var users = JsonUtility.FromJson<talkusers>(DbProcess.returnText);


        foreach (var talk in users.talks)
        {
            if (talk.user_id != login.userId)
            {
                Debug.Log(talk.user_id);
                Debug.Log(talk.user_name);

                var item = GameObject.Instantiate(prefab) as RectTransform;
                item.SetParent(transform, false);

                var text = item.GetComponentInChildren<Text>();
                item.name = talk.user_id;
                text.text = talk.user_name;
            }


        }

        yield return null;
    }

    public void backScene()
    {
        SceneManager.LoadScene("top");
    }
}

[Serializable]

public class talkuser
{
    public string user_name;
    public string user_id;
}

[Serializable]
public class talkusers
{
    public List<talkuser> talks;
}