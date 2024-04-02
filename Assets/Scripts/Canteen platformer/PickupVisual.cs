using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Visual
{
    public class PickupVisual : MonoBehaviour
    {
        private int remainsToCollect = 4;
        [SerializeField] private TextMeshProUGUI text;
        public SceneData sceneData;
        public VectorValue pos;

        public void UpdateAmount()
        {
            --remainsToCollect;
            text.text = "Осталось собрать: " + remainsToCollect;
            if (remainsToCollect == 0)
            {
                pos.value = new Vector3(2.48f, -3.16f, 0);
                sceneData.numOfCanteenTask = 2;
                SceneManager.LoadScene(7);
            }
        }
    }
}
