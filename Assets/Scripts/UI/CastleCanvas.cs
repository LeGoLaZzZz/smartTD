using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CastleCanvas : MonoBehaviour
    {
        [SerializeField] private Text lifeText;

        public void SetLifes(float lifes)
        {
            lifeText.text = lifes.ToString();
        }
    }
}