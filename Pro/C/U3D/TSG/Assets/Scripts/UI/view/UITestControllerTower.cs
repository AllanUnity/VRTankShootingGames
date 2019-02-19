using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestControllerTower : MonoBehaviour
{
    public TKTowerModelBase tower;
    public InputField towerInputField;
    public InputField gunInputField;
    public Button countersignBtn;
    // Start is called before the first frame update
    void Start()
    {
        countersignBtn.onClick.AddListener(() =>
        {
            float towerF = float.Parse(towerInputField.text);
            float gunF = float.Parse(gunInputField.text);

            tower.RotateTower(towerF, gunF);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
