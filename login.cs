using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    public static string userId;                    　　　　　　　　　　　　　　　　　　　　//ユーザーID
    public static int loginFlg;                                         //ログインフラグ

    [SerializeField] public InputField userName;    　　　　　　　　　　　　　　　　　　　　//ユーザ名インプットフィールド
    [SerializeField] public InputField password;    　　　　　　　　　　　　　　　　　　　　//パスワードインプットフィールド
    [SerializeField] public Text notText;              　　　　　　　　　　　　　　　　　   //パスワード間違いテキスト

    public void loginFunc()
    {
        StartCoroutine(loginCoro());
    }

    private IEnumerator loginCoro()
    {
        yield return StartCoroutine(getPass());

        StartCoroutine(result());
    }

    private IEnumerator getPass()
    {
        var ins = new DbProcess();

        var dic = new Dictionary<string, string>();

        dic.Add("name", userName.text);
        ins.Dic = dic;

        ins.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/login.php";
        var obj = gameObject.AddComponent<DbProcess>();

        yield return StartCoroutine(obj.process(ins.Dic, ins.ServerAddress));
    }

    private IEnumerator result()
    {

        var ins = new common();
        string cuted = ins.cutStr(DbProcess.returnText);

        var passStr = JsonUtility.FromJson<PassClass>(cuted);
        if (passStr != null)
        {
            string pass = passStr.password;

            if (pass == password.text)
            {
                userId = passStr.id;
                loginFlg = 1;
                SceneManager.LoadScene("top");
            }
            else
            {
                notText.gameObject.SetActive(true);
            }
        }
        else
        {
            notText.gameObject.SetActive(true);
        }

        yield return null;
    }

    public void makeAccountScene()
    {
        SceneManager.LoadScene("makeAccount");
    }
}

[System.Serializable]
public class PassClass
{
    public string password;
    public string id;
}