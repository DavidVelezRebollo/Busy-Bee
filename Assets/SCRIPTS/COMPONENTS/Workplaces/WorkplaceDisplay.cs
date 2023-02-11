using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkplaceDisplay : MonoBehaviour
{
    public Workplace workplaceType;

    public Text nameText;
    public Text descriptionText;

    public Image workplaceSprite;

    public Text honeyProductionText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = workplaceType.name;
        descriptionText.text = workplaceType.description;
        workplaceSprite.sprite = workplaceType.workplaceSprite;

        honeyProductionText.text = workplaceType.honeyProduction.ToString();
    }
}