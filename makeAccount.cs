using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class makeAccount : MonoBehaviour
{
    [SerializeField] private InputField userName;                                                 //ユーザ名インプットフィールド
    [SerializeField] private InputField password;                                                 //パスワードインプットフィールド
    [SerializeField] public Text notText;                                                         //ユーザー名被りテキスト
    [SerializeField] public Button registerButton;                                                //登録ボタン

    public static string name;                                                                    //ユーザー名変数
    public static string pass;                                                                    //パスワード変数

    // Start is called before the first frame update
    void Start()
    {
        //初期表示は非活性
        registerButton.enabled = false;
    }

    public void veriFunc()
    {
        StartCoroutine(verification());
    }

    private IEnumerator verification()
    {
        yield return StartCoroutine(register());

        StartCoroutine(result());
    }

    private IEnumerator register()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("name", userName.text);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/account_verification.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        if (DbProcess.returnText != "null")
        {
            notText.gameObject.SetActive(true);
            registerButton.enabled = false;
        }
        else
        {
            notText.gameObject.SetActive(false);
            registerButton.enabled = true;
        }

        yield return null;
    }

    public void makeVeriScene()
    {

        if (userName.text == "")
        {
            notText.text = "ユーザー名を入力してください";
            notText.gameObject.SetActive(true);
        }
        else if (password.text == "")
        {
            notText.text = "パスワードを入力してください";
            notText.gameObject.SetActive(true);

        }
        else
        {
            name = userName.text;
            pass = password.text;
            SceneManager.LoadScene("makeAccountVeri");
        }

    }

    public void backScene()
    {
        SceneManager.LoadScene("login");
    }
}
