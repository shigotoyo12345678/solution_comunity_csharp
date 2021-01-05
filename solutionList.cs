using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class solutionList : MonoBehaviour
{

    [SerializeField] RectTransform prefab = null;                                             //ソリューションボタン

    IEnumerator Start()
    {
        yield return StartCoroutine(register());

        StartCoroutine(result());
    }

    private IEnumerator register()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/solutionGet.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        var solutions = JsonUtility.FromJson<solutions>(DbProcess.returnText);

        foreach (var solution in solutions.solution)
        {
            Debug.Log(solution.solution_name);

            var item = GameObject.Instantiate(prefab) as RectTransform;
            item.SetParent(transform, false);

            var text = item.GetComponentInChildren<Text>();
            item.name = solution.solution_id;
            text.text = solution.solution_name;

        }

        yield return null;
    }

    public void backScene()
    {
        SceneManager.LoadScene("top");
    }

}

[Serializable]

public class solution
{
    public string solution_name;
    public string solution_id;
}

[Serializable]
public class solutions
{
    public List<solution> solution;
}