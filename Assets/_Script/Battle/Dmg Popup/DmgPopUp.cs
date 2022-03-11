using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PH.PopUp
{
    public class DmgPopUp : MonoBehaviour
    {
        private static int sortingOder;

        private const float DISAPPEAR_MAX = 1f;
        private const float INCREASE_SCALE = 1f;
        private const float DECREASE_SCALE = 0.5f;
        private const float DISPAPPEAR_SPEED = 1.6f;
        private const float MOVE_VECTOR_X = 0.7f;
        private const float MOVE_VECTOR_Y = 1;
        private const float MOVE_DISTANCE = 8f;
        private const float MOVE_VECTOR_OFFSET = 20f;
        private const int TEXT_SIZE_SMALL = 13;
        private const int TEXT_SIZE_MEDIUM = 14;
        private const int TEXT_SIZE_LARGE = 15;
        private const float SPAWN_OFSET_X = 6f;
        private const string format_CRIT = "<sprite=" + "\"crit\"" + " index=0 >";

        //private readonly Color colorPhysical = new Color32(255, 111, 52, 255);
        private readonly Color colorPhysical = new Color32(255, 133, 0, 255);

        private float disappearTime;
        private Vector3 moveVector;
        private TextMeshPro textMesh;
        private Color textColor;
        private Transform cam;

        private void Awake()
        {
            textMesh = transform.GetComponent<TextMeshPro>();
            cam = Camera.main.transform;
        }

        private void Update()
        {
            transform.position += moveVector * Time.deltaTime;
            moveVector -= MOVE_DISTANCE * Time.deltaTime * moveVector;

            disappearTime -= Time.deltaTime;
            ScaleByDisappear();
            FadeByDisappear();
        }

        private void FadeByDisappear()
        {
            if (disappearTime <= 0)
            {
                textColor.a -= DISPAPPEAR_SPEED * Time.deltaTime;
                textMesh.color = textColor;

                if (textColor.a <= 0)
                {
                    DmgPopUpPool.Instance.ReturnPool(this);
                }
            }
        }

        private void ScaleByDisappear()
        {
            if (disappearTime > DISAPPEAR_MAX / 2)
            {
                // First half of the popup lifetime
                transform.localScale += INCREASE_SCALE * Time.deltaTime * Vector3.one;
            }
            else
            {
                //Second half of the popup lifetime
                transform.localScale -= DECREASE_SCALE * Time.deltaTime * Vector3.one;
            }
        }


        public void SetUp(int amount, DamageType type, Vector3 spawnPos)
        {
            SetUpTextColor(amount, type);
            SetUpTextSize(amount);
            SetUpTransform(spawnPos);
            SetUpVectorMove(type);

            disappearTime = DISAPPEAR_MAX;

            sortingOder++;
            textMesh.sortingOrder = sortingOder;

            gameObject.SetActive(true);
        }

        public void SetUpCrit(int amount, Vector3 spawnPos)
        {
            SetUpTransform(spawnPos);
            SetUpCritTextColor(amount);
            SetUpTextSize(amount);

            disappearTime = DISAPPEAR_MAX;

            sortingOder++;
            textMesh.sortingOrder = sortingOder;

            //pop-up to top right
            moveVector = new Vector3(MOVE_VECTOR_X, MOVE_VECTOR_Y) * MOVE_VECTOR_OFFSET;

            gameObject.SetActive(true);
        }

        private void SetUpTransform(Vector3 spawnPos)
        {
            transform.position = new Vector3(spawnPos.x, spawnPos.y + SPAWN_OFSET_X, spawnPos.z);
            transform.localScale = Vector3.one;
            transform.LookAt(transform.position + cam.forward);
        }

        private void SetUpTextColor(int amount, DamageType damageType)
        {
            textMesh.color = damageType.GetColor();

            if(damageType is HealDmg)
            {
                textMesh.SetText("+" + amount.ToString());
            }
            else
            {
                textMesh.SetText(amount.ToString());
            }

            textColor = textMesh.color;
        }

        private void SetUpVectorMove(DamageType dmgType)
        {
            if (! (dmgType as HealDmg))
            {   
                //top right
                moveVector = new Vector3(MOVE_VECTOR_X, MOVE_VECTOR_Y) * MOVE_VECTOR_OFFSET;
            }
            else
            {   
                //top up
                moveVector = new Vector3(0, 10f);
            }
        }

        private void SetUpCritTextColor(int amount)
        {
            textMesh.color = colorPhysical;
            textMesh.SetText(format_CRIT + amount.ToString());
        }

        private void SetUpTextSize(int amount)
        {
            if (amount <= 9)
            {
                textMesh.fontSize = TEXT_SIZE_SMALL;
            }
            else if (amount <= 99)
            {
                textMesh.fontSize = TEXT_SIZE_MEDIUM;
            }
            else
            {
                textMesh.fontSize = TEXT_SIZE_LARGE;
            }
        }
    }
}

