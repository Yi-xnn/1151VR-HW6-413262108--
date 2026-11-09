using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [Header("直接把文字物件拖進來（任何文字都吃）")]
    public GameObject timerObject;
    public GameObject pointObject;

    public float time = 30.0f;
    private int point = 0;

    public void GetApple()
    {
        this.point += 100;
    }

    public void GetBomb()
    {
        this.point /= 2;
    }

    void Update()
    {
        if (this.time > 0)
        {
            this.time -= Time.deltaTime;

            if (this.time <= 0)
            {
                this.time = 0;
                GameObject generator = GameObject.Find("ItemGenerator");
                if (generator != null)
                {
                    Destroy(generator);
                }
            }
        }

        // 自動判斷是一般 Text 還是 TextMeshPro
        UpdateTextUI(timerObject, this.time.ToString("F1"));
        UpdateTextUI(pointObject, this.point.ToString() + " point");
    }

    void UpdateTextUI(GameObject obj, string text)
    {
        if (obj == null) return;

        // 嘗試更新 TextMeshPro
        var tmp = obj.GetComponent<TextMeshProUGUI>();
        if (tmp != null)
        {
            tmp.text = text;
            return;
        }

        // 嘗試更新傳統 UI Text
        var txt = obj.GetComponent<Text>();
        if (txt != null)
        {
            txt.text = text;
        }
    }
}