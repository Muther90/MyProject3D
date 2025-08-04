using System.Collections;
using UnityEngine;

public static class Timer 
{
    private static readonly WaitForSeconds WaitForSecond = new WaitForSeconds(1f);

    public static IEnumerator WaitSeconds(int to)
    {
        for (int i = 0; i < to; i++)
        {
            yield return WaitForSecond;
        }
    }
}
