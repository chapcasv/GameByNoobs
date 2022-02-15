using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PH.PopUp
{   
    public enum TypeText
    {
        Dot,
        Heal,
        Physical,
        Magic,
        TrueDmg
    }

    public class DmgPopUp : MonoBehaviour
    {
        private TextMeshPro textMesh;
        private float moveUpSpeed = 1f;
        [SerializeField] private float disappearTime = 0.8f;
        private float disappearSpeed = 1.2f;
        private Color textColor;
        private readonly Color colorHeal = new Color32(83, 255, 98, 255);
        private readonly Color colorPhysical = new Color32(255, 111, 52, 255);
        private readonly Color colorMagic = new Color32(98, 239, 255, 255);
        private Transform cam;

        private void Awake()
        {
            textMesh = transform.GetComponent<TextMeshPro>();
            cam = Camera.main.transform;
        }

        private void Update()
        {
            transform.position += new Vector3(0, moveUpSpeed) * Time.deltaTime;

            disappearTime -= Time.deltaTime;
            if (disappearTime <= 0)
            {
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a <= 0)
                {
                    DmgPopUpPool.Instance.ReturnPool(this);
                }
            }
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + cam.forward);
        }

        public void SetUp(int amount, DmgType type, Vector3 spawnPos)
        {
            switch (type)
            {
                case DmgType.Physical:
                    SetUpPhysicalColor(amount);
                    break;
                case DmgType.Magic:
                    SetUpMagicColor(amount);
                    break;
                case DmgType.TrueDmg:
                    break;
                case DmgType.DoT:
                    break;
                case DmgType.Heal:
                    SetUpHealColor(amount);
                    break;
            }

            transform.position = spawnPos;
            disappearTime = 0.8f;
            textColor = textMesh.color;
            gameObject.SetActive(true);
        }


        private void SetUpPhysicalColor(int amount)
        {
            textMesh.color = colorPhysical;
            textMesh.SetText(amount.ToString());
        }

        private void SetUpHealColor(int amount)
        {
            textMesh.color = colorHeal;
            textMesh.SetText("+" + amount.ToString());
        }

        private void SetUpMagicColor(int amount)
        {
            textMesh.color = colorMagic;
            textMesh.SetText(amount.ToString());
        }

    }
}

