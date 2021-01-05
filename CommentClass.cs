using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommentClass : MonoBehaviour
{

    [SerializeField] public InputField commentInput;                                        //コメント入力インプットフィールド
    [SerializeField] RectTransform prefab = null;                                           //コメント表示テキスト
    [SerializeField] public GameObject commentPanel;                                        //コメント表示パネル

    IEnumerator Start()
    {
        yield return StartCoroutine(getComment());

        StartCoroutine(result());
    }

    private IEnumerator getComment()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("solution_id", common.solution_id);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/getComment.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        var comments = JsonUtility.FromJson<comments>(DbProcess.returnText);


        foreach (var comment in comments.comment)
        {
            var item = GameObject.Instantiate(prefab) as RectTransform;
            item.SetParent(transform, true);

            Text text = item.GetComponentInChildren<Text>();

            text.text = comment.commentText;

        }

        yield return null;
    }

    //コメント投稿関数
    public void commentPostFunc()
    {
        //コメントインプットフィールドが未入力の場合は処理を行わない
        if (commentInput.text != "")
        {
            //DbProcess.dic.Clear();

            StartCoroutine(commentPost());
        }

    }

    private IEnumerator commentPost()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("solution_id", common.solution_id);
        dic.Add("user_id", login.userId);
        dic.Add("comment", commentInput.text);

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/commentPost.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));

        commentPanel.gameObject.SetActive(false);
        SceneManager.LoadScene("solution");

    }
}

[Serializable]

public class Comment
{
    // public string username;
    public string commentText;
}

[Serializable]

public class comments
{
    public List<Comment> comment;
}