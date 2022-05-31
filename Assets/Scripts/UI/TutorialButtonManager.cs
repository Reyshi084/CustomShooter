using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonManager : MonoBehaviour
{
    private GameObject _nowFilter;

    public void SetInfo(GameObject filter)
    {
        _nowFilter = filter;
    }

    public void OnBackButtonDown()
    {
        if (_nowFilter != null)
        {
            _nowFilter.SetActive(false);
            Time.timeScale = 1;
        }
        Destroy(this.gameObject);
    }
}
