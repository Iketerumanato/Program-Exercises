using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTest : MonoBehaviour
{
    void Start()
    {
        // 4文字以上の文字列を探して字数のリストを作る
        List<string> source = new() { "Bird", "Dog", "Cat", "Fish", "Horse" };

        // -- foreach版 --
        //List<int> forResult = new List<int>();
        //foreach (string s in source)
        //{
        //    int length = s.Length;
        //    if (length >= 4)
        //    {
        //        forResult.Add(length);
        //    }
        //}
        //foreach (int result in forResult)
        //{
        //    Debug.Log(result.ToString());
        //}

        // -- LINQ版 --
        IEnumerable<int> linqResult = source.Select(s => s.Length)
            .Where(l => l >= 4);
        foreach (int result in linqResult)
        {
            Debug.Log(result);
        }
    }
}