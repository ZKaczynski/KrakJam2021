using System.Collections;
using UnityEngine;

namespace UI
{
    public class FaderScreen : UIScreen
    {
        private void OnEnable()
        {
            StartCoroutine(COR_Close());
        }

        private IEnumerator COR_Close()
        {
            yield return new WaitForSeconds(3);
            Close();
        }
    }
}
