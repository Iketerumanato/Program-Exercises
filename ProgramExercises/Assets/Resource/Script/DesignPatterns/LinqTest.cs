using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTest : MonoBehaviour
{
    // 4文字以上の文字列を探して字数のリストを作る
    [SerializeField]
    List<string> source = new() { "Bird", "Dog", "Cat", "Fish", "Horse" };

    [SerializeField] int serchCharNum = 4;

    void Start()
    {
        #region -- foreach版 --
        //List<int> forResult = new List<int>();
        //foreach (string strAnimalName in source)
        //{
        //    int length = strAnimalName.Length;
        //    if (length >= serchCharNum)
        //    {
        //        forResult.Add(length);
        //    }
        //}
        //foreach (int result in forResult)
        //{
        //    Debug.Log(result.ToString());
        //}
        #endregion

        #region -- LINQ版 --
        IEnumerable<int> linqResult = source.Select(strAnimalName => strAnimalName.Length)
            .Where(charNum => charNum >= serchCharNum);
        foreach (int result in linqResult)
        {
            Debug.Log(result);
        }
        #endregion
    }
}