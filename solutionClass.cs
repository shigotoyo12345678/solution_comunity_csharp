using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class solutionClass : MonoBehaviour
{

    public static string yourId;
    public static string userName;
    public static string myFlg;
    public static string commentText;

    public static string commentFlg;

    [SerializeField] public Text userNameText;
    [SerializeField] public Text solutionTitle;
    [SerializeField] public Text content;
    [SerializeField] public Text nice;
    [SerializeField] public Button niceBtn;
    [SerializeField] public GameObject commentPanel;
    [SerializeField] public InputField commentInput;
    [SerializeField] RectTransform prefab = null;

    IEnumerator Start()
    {
        goTalkButton.userName = null;
        goTalkButton.yourId = null;

        yield return StartCoroutine(registerSolution());

        StartCoroutine(result());
    }

    private IEnumerator registerSolution()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("solution_id", common.solution_id);
        dic.Add("user_id", login.userId);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/getSolutionDetail.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        string a = DbProcess.returnText;

        string b = a.Remove(0, 1);
        string d = b.Remove(b.Length - 1, 1); ///文字列の調整

        var solutionDetail = JsonUtility.FromJson<solutionDetail>(d);

        yourId = solutionDetail.user_id;
        userNameText.text = solutionDetail.user_name;
        solutionTitle.text = solutionDetail.solution_name;
        content.text = solutionDetail.solution;
        if (solutionDetail.niceSum == "")
        {
            nice.text = "0";
        }
        else
        {
            nice.text = solutionDetail.niceSum;
        }
        if (solutionDetail.myFlg == "")
        {
            myFlg = "0";
        }
        else
        {
            myFlg = solutionDetail.myFlg;
            niceBtn.GetComponent<Image>().color = Color.red;
        }
        Debug.Log(solutionDetail.myFlg);

        yield return null;
    }

    public void backScene()
    {
        SceneManager.LoadScene("solutionList");
    }


    public void talkScene()
    {
        if (yourId == login.userId)
        {
            Debug.Log("自分とのトークはできません");
        }
        else
        {
            userName = userNameText.text;
            SceneManager.LoadScene("talk");
        }

    }

    public void commentDisplayOpen()
    {
        commentPanel.gameObject.SetActive(true);
    }

    public void commentDisplayClose()
    {
        commentPanel.gameObject.SetActive(false);
    }

}

[Serializable]
public class solutionDetail
{
    public string user_id;
    public string user_name;
    public string solution_name;
    public string solution;
    public string niceSum;
    public string myFlg;
}

