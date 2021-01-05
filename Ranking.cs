using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{

    [SerializeField] public GameObject solutionBtn;

    IEnumerator Start()
    {
        yield return StartCoroutine(getRanking());

        StartCoroutine(result());
    }

    private IEnumerator getRanking()
    {
        DbProcess obj = gameObject.AddComponent<DbProcess>();
        var dic = new Dictionary<string, string>();

        obj.Dic = dic;

        obj.ServerAddress = "http://shigotoyo.starfree.jp/solution_comunity/getRanking.php";

        yield return StartCoroutine(obj.process(obj.Dic, obj.ServerAddress));
    }

    private IEnumerator result()
    {
        var solutionRankingList = JsonUtility.FromJson<solutionRankings>(DbProcess.returnText);


        float x = -8f;
        float y = 600;
        int i = 0;

        foreach (var solutionRanking in solutionRankingList.ranking)
        {
            if (i < 5)
            {
                Debug.Log(solutionRanking.solution_name);
                // Debug.Log(solutionRanking.solution);
                // Debug.Log(solutionRanking.sumnice);
                Debug.Log(solutionRanking.solution_id);

                y = y - 250f;

                buttonMake(solutionRanking, x, y);

                i++;
            }



        }

        yield return null;
    }

    public void buttonMake(solutionRanking solutionRanking, float x, float y)
    {
        solutionBtn.transform.FindChild("Text").GetComponent<Text>().text = solutionRanking.solution_name;

        // プレハブを元にオブジェクトを生成する
        GameObject instance = (GameObject)Instantiate(solutionBtn,
                                                      new Vector3(x, y, 0.0f),
                                                      Quaternion.identity);
        instance.name = solutionRanking.solution_id;

        GameObject canvas = GameObject.Find("Canvas"); //Canvasを探して、canvasとして定義
        instance.transform.SetParent(canvas.transform, false); //複製したボタンをcanvasに格納


    }

    public void backScene()
    {
        SceneManager.LoadScene("top");
    }
}

[Serializable]

public class solutionRanking
{
    public string solution_name;
    //public string solution;
    //public string sumnice;
    public string solution_id;
}

[Serializable]
public class solutionRankings
{
    public List<solutionRanking> ranking;
}
