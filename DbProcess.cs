using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DbProcess : MonoBehaviour
{
    private Dictionary<string, string> dic; //送信用

    private string serverAddress = "";                                                    //送信用アドレス
    public static string returnText = "";                                                       //結果用変数

    public Dictionary<string, string> Dic
    {
        set { dic = value; } //値の代入
        get { return dic; } //外部に値を返す
    }
    public string ServerAddress
    {
        set { serverAddress = value; } //値の代入
        get { return serverAddress; } //外部に値を返す
    }

    public IEnumerator process(Dictionary<string, string> dic, string serverAddress)
    {

        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in dic)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        UnityWebRequest request = UnityWebRequest.Post(serverAddress, form);

        yield return request.SendWebRequest();

        if (request.isHttpError || request.isNetworkError)
        {
            //4.エラー確認
        }
        else
        {
            //4.結果確認
            returnText = request.downloadHandler.text;
        }

    }
}
