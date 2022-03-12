using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class BattleLifeUI : MonoBehaviour
    {
        [SerializeField] float during = 0.8f;
        [SerializeField] TextMeshProUGUI enemyLifeText;
        [SerializeField] TextMeshProUGUI playerLifeText;
        [SerializeField] Image enemyHealthBar;
        [SerializeField] Image playerHealthBar;
        [SerializeField] Vector2 scaleBig = new Vector2(1.5f, 1.5f);
        [SerializeField] float scaleSpeed = 0.4f;
        [SerializeField] Ease easeMode = Ease.InOutBack;
        [SerializeField] float delay = 0.3f;

        private LifeSystem _lifeSystem;
        private int oldLifeEnemy;
        private int oldLifePlayer;
        private int maxEnemyHelath;
        private int maxPlayerHealth;

        public void Constructor(LifeSystem LS)
        {
            _lifeSystem = LS;

            oldLifeEnemy= maxEnemyHelath = _lifeSystem.GetEnemyLife();
            oldLifePlayer= maxPlayerHealth = _lifeSystem.GetPlayerLife();

            AddListerner();
            DisplayEnemyLife();
            DisplayPlayerLife();
        }

        private void DisplayEnemyLife()
        {   
            int currentLife = _lifeSystem.GetEnemyLife();
            DOVirtual.Int(oldLifeEnemy, currentLife, during, (v) => enemyLifeText.text = v.ToString())
                .SetDelay(delay);
            ScaleBig(enemyLifeText.transform);
            SliderHealth(maxEnemyHelath, currentLife, enemyHealthBar, scaleSpeed);
            oldLifeEnemy = currentLife;
        }

        private void DisplayPlayerLife()
        {
            int currentLife = _lifeSystem.GetPlayerLife();
            DOVirtual.Int(oldLifePlayer, currentLife, during, (v) => playerLifeText.text = v.ToString());
          
            oldLifePlayer = currentLife;
            ScaleBig(playerLifeText.transform);
            SliderHealth(maxPlayerHealth, currentLife, playerHealthBar, scaleSpeed);
        }
        private void SliderHealth(int maxHealth, int curhealth, Image health, float delay)
        {
            float healthPercent = (float)curhealth / (float)maxHealth;
            float percent = (float)Mathf.Round(healthPercent * 100f) / 100f;
            health.DOFillAmount(percent, during).SetDelay(delay);
        }
        private void ScaleBig(Transform transform)
        {
            transform.DOScale(scaleBig, scaleSpeed).SetEase(easeMode)
                .OnComplete(() => ScaleSmall(transform));
        }

        private void ScaleSmall(Transform transform)
        {
            transform.DOScale(Vector2.one, scaleSpeed).SetEase(easeMode);
            
        }

        private void AddListerner()
        {
            _lifeSystem.OnEnemyLifeChange += DisplayEnemyLife;
            _lifeSystem.OnPlayerLifeChange += DisplayPlayerLife;
        }

        private void RemoveListerner()
        {
            _lifeSystem.OnEnemyLifeChange -= DisplayEnemyLife;
            _lifeSystem.OnPlayerLifeChange -= DisplayPlayerLife;
        }

        private void OnDisable()
        {
            RemoveListerner();
        }
    }
}

