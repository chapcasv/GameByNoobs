using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitHealthBar : MonoBehaviour
    {
        [SerializeField] Image HpBar;
        [SerializeField] Image HpShrink;
        private const float SHRINK_SPEED = 0.5f;
        private UnitSurvivalStat health;
        private HealthSegmentLayout healthSegment;
        private void Awake()
        {
            health = GetComponentInParent<UnitSurvivalStat>();
            healthSegment = GetComponentInChildren<HealthSegmentLayout>();
        }

        private void OnEnable()
        {
            Reload();

            health.OnTakeDamage += ChangeOnTakeDamage;
            health.OnHealthUp += ChangeOnHealthUp;
        }

        private void ChangeOnTakeDamage(float pct)
        {
            HpBar.fillAmount = pct;
            StartCoroutine(ChangeToPct(pct));
        }

        private void ChangeOnHealthUp(float pct) 
        {
            HpBar.fillAmount = pct;
            healthSegment.UpdateSegment(health.ORMaxHP);
        } 

        private IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = HpShrink.fillAmount;
            float elapsed = 0f;

            while (elapsed < SHRINK_SPEED)
            {
                elapsed += Time.deltaTime;
                HpShrink.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / SHRINK_SPEED);

                yield return null;
            }

            HpShrink.fillAmount = pct;
        }

        private void Reload()
        {
            if (health.ORMaxHP == 0) return;
           
            float pct = health.ORCurrentHP / health.ORMaxHP;
            HpBar.fillAmount = pct;
        }

        private void OnDisable()
        {
            health.OnHealthUp -= ChangeOnHealthUp;
            health.OnTakeDamage -= ChangeOnTakeDamage;
        }
    }
}

