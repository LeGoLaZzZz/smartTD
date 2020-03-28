using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutintest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        tt.test = this;
        tt.kal();
    }


    public Coroutintesttt tt;



    public void StartCours(IEnumerator cour)
    {
        StartCoroutine(cour);
    }

    public void Instmyobj(GameObject go)
    {
        Instantiate(go);
    }
}

[CreateAssetMenu(menuName = "MyObjects/Coroutintesttt", fileName = "Coroutintesttt1")]
public class Coroutintesttt : ScriptableObject
{
    public Coroutintest test;
    public GameObject prefab;

    public void kal()
    {
        test.StartCours(cour());
    }


    IEnumerator cour()
    {
        yield return new WaitForSeconds(1f);
        test.Instmyobj(prefab);
    }
}