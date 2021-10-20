using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTail : MonoBehaviour
{
    private void Update()
    {
        BuildTail();
    }
    void BuildTail()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Player.weaponTail != null)
            {
                if (Player.weaponTail.transform.position.y > transform.GetChild(i).position.y)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
                transform.GetChild(i).position = new Vector2(Player.instance.transform.position.x, transform.GetChild(i).position.y);
            }

        }
    }
}
