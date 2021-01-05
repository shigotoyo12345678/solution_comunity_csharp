using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class common : MonoBehaviour
{
    [SerializeField] public GameObject goTalkBtn;                                             //トークボタン
    [SerializeField] public Button solutionBtn;                                               //ソリューションボタン

    public static string solution_id;                                                         //ソリューションID 

    //文字列の調整
    public string cutStr(string phpText)
    {
        string a = phpText;
        string b = a.Remove(0, 1);
        string c = b.Remove(b.Length - 1, 1);

        return c;
    }

    //トップ画像クリック処理
    public void topImageScene()
    {
        switch (login.loginFlg)
        {
            case 0:
                SceneManager.LoadScene("login");
                break;

            case 1:
                SceneManager.LoadScene("top");
                break;
        }
    }

    public void solutionScene()
    {
        Debug.Log(solutionBtn.name);
        solution_id = solutionBtn.name;
        SceneManager.LoadScene("solution");
    }

    public void goTalkScene()
    {
        var ins = new goTalkButton();

        ins.goTalk(goTalkBtn);
    }

}

public class goTalkButton : MonoBehaviour
{

    public static string userName;
    public static string yourId;

    public void goTalk(GameObject goTalkBtn)
    {
        var text = goTalkBtn.GetComponentInChildren<Text>();
        yourId = goTalkBtn.name;
        userName = text.text;
        SceneManager.LoadScene("talk");
    }
}

