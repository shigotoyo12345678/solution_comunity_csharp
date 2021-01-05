using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class top : MonoBehaviour
{

    [SerializeField] private Text nameText;                                              //ユーザ名テキスト

    IEnumerator Start()
    {
        yield return StartCoroutine(getUserName());

        StartCoroutine(result());
    }

    private IEnumerator getUserName()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("me_id", login.userId);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/top.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        var ins = new common();
        string cuted = ins.cutStr(DbProcess.returnText);
        var name = JsonUtility.FromJson<UserClass>(cuted);
        nameText.text = name.name;

        yield return null;
    }

    public void logout()
    {
        login.userId = null;
        login.loginFlg = 0;
        SceneManager.LoadScene("login");
    }

    public void postScene()
    {
        SceneManager.LoadScene("postInput");
    }

    public void solutionListScene()
    {
        SceneManager.LoadScene("solutionList");
    }

    public void talkListScene()
    {
        SceneManager.LoadScene("talkList");
    }

    public void rankingScene()
    {
        SceneManager.LoadScene("ranking");
    }
}

[System.Serializable]
public class UserClass
{
    public string name;
}