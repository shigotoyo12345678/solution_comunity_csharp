using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nice : MonoBehaviour
{

    public void niceFunc()
    {
        StartCoroutine(nice());
    }

    private IEnumerator nice()
    {
        yield return StartCoroutine(register());

        StartCoroutine(result());
    }

    private IEnumerator register()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        dic.Add("solution_id", common.solution_id);
        dic.Add("user_id", login.userId);
        dic.Add("niceFlg", solutionClass.myFlg.ToString());

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/niceProcess.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        Debug.Log(DbProcess.returnText);
        SceneManager.LoadScene("solution");

        yield return null;
    }


}
