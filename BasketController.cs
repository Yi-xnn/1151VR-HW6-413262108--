using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 加入這行來支援新版 Input System

public class BasketController : MonoBehaviour
{
    public AudioClip appleSE;
    public AudioClip bombSE;
    AudioSource aud;
    GameObject director;

    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        this.aud = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Apple")
        {
            this.director.GetComponent<GameDirector>().GetApple();
            this.aud.PlayOneShot(this.appleSE);
        }
        else
        {
            this.director.GetComponent<GameDirector>().GetBomb();
            this.aud.PlayOneShot(this.bombSE);
        }
        Destroy(other.gameObject);
    }

    void Update()
    {
        // 確保滑鼠存在，並且左鍵在當前影格被按下
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 取得目前滑鼠在螢幕上的 2D 座標
            Vector2 mousePosition2D = Mouse.current.position.ReadValue();
            Vector3 mousePosition = new Vector3(mousePosition2D.x, mousePosition2D.y, 0f);

            // 將螢幕座標轉換為射線
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            // 發射射線偵測碰撞
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // 讓籃子瞬移到點擊的整數格子上
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);
                transform.position = new Vector3(x, 0, z);
            }
        }
    }
}