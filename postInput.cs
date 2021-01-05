using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class postInput : MonoBehaviour
{
    [SerializeField] public InputField titleInput;      //タイトルインプットフィールド
    [SerializeField] public InputField contentInput;    //内容インプットフィールド
    [SerializeField] public Text notText;

    public static string title;                         //タイトル保持用変数
    public static string solution;                       //ソリューション保持用変数

    public static int sceneNum = 0;

    void Start()
    {
        if (sceneNum == 0)
        {
            titleInput.text = "";
            contentInput.text = "";
        }
        else if (sceneNum == 1)
        {
            titleInput.text = title;
            contentInput.text = solution;
        }
    }

    public void sceneChenge()
    {
        if (titleInput.text == "")
        {
            notText.gameObject.SetActive(true);
            notText.text = "題名を入力してください";
        }
        else if (contentInput.text == "")
        {
            notText.gameObject.SetActive(true);
            notText.text = "内容を入力してください";
        }
        else
        {
            notText.gameObject.SetActive(false);
            title = titleInput.text;
            solution = contentInput.text;
            SceneManager.LoadScene("postVerification");
        }


    }

    public void backScene()
    {
        SceneManager.LoadScene("top");
    }
}
