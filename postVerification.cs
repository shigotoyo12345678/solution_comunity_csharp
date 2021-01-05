using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class postVerification : MonoBehaviour
{
    [SerializeField] public Text titleText;                                                     //タイトルテキスト
    [SerializeField] public Text contentText;                                                   //内容テキスト

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = postInput.title;
        contentText.text = postInput.solution;
    }

    public void postFanc()
    {
        StartCoroutine(post());
    }

    private IEnumerator post()
    {
        yield return StartCoroutine(register());

        StartCoroutine(result());
    }

    private IEnumerator register()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("user_id", login.userId);
        dic.Add("title", postInput.title);
        dic.Add("content", postInput.solution);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/registerContent.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        SceneManager.LoadScene("top");

        yield return null;
    }

    public void backScene()
    {
        postInput.sceneNum = 1;
        SceneManager.LoadScene("postInput");
    }
}
