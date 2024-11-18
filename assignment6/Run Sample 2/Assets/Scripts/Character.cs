using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    const float CharacterJumpPower = 7f;
    const int MaxJump = 2;
    int RemainJump = 0;
    [SerializeField] GameManager GM;
    [SerializeField] UIManager MyUIManager;

    void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 RemainJump를 하나 소모하여 CharacterJumpPower의 힘으로 점프한다.

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RemainJump--;
            Jump(CharacterJumpPower);
        }

    }

    // Jump with power
    void Jump(float power)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector3(0, CharacterJumpPower, 0), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform")
        {
            RemainJump=MaxJump;
        }
        else if (col.gameObject.tag == "Obstacle")
        {
            GM.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Point")
        {
            GM.NowScore++;
            MyUIManager.DisplayScore(GM.NowScore);
            Destroy(col.gameObject);
        }
    }
}
