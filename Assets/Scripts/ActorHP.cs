using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActorHP : MonoBehaviour
{
    public TextMeshProUGUI HPText;
    public float HP;
    private void Awake()
    {
        if (HPText)
            HPText.text = HP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetHP(float value)
    {
        HP += value;
        if (HPText)
        {
            HPText.text = HP.ToString();
        }
    }
}
