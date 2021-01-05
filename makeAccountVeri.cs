using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class makeAccountVeri : MonoBehaviour
{

    [SerializeField] public Text text;                                                       //登録可否テキスト

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(registerAccount());

        StartCoroutine(result());
    }

    private IEnumerator registerAccount()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("name", makeAccount.name);
        dic.Add("pass", makeAccount.pass);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/account.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        if (DbProcess.returnText != "")
        {
            text.text = DbProcess.returnText;
        }
        else
        {
            text.text = "登録できませんでした";
        }


        yield return null;
    }

    public void loginScene()
    {
        SceneManager.LoadScene("login");
    }
}
