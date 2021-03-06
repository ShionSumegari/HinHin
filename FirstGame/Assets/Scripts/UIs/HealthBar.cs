using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private float maxHealthPoints;

    [SerializeField]
    private float healthBarStepsLength = 100;

    [SerializeField]
    private float damagesDecreaseRate = 10;

    private float currentHealthPoints;

    public RectTransform imageRectTransform;

    private float damages;

    public float Health
    {
        get { return currentHealthPoints; }
        set
        {
            currentHealthPoints = Mathf.Clamp(value, 0, MaxHealthPoints);
            image.material.SetFloat("_Percent", currentHealthPoints / MaxHealthPoints);

            if (currentHealthPoints < Mathf.Epsilon)
                Damages = 0;
        }
    }

    public float Damages
    {
        get { return damages; }
        set
        {
            damages = Mathf.Clamp(value, 0, MaxHealthPoints);
            image.material.SetFloat("_DamagesPercent", damages / MaxHealthPoints);
        }
    }

    public float MaxHealthPoints
    {
        get { return maxHealthPoints; }
        set
        {
            maxHealthPoints = value;
            if(maxHealthPoints < 1000f)
            {
                healthBarStepsLength = 100f;
            } else
            {
                healthBarStepsLength = 150f;
            }
            image.material.SetFloat("_Steps", MaxHealthPoints / healthBarStepsLength);
        }
    }

    protected void Awake()
    {
        imageRectTransform = image.GetComponent<RectTransform>();
        image.material = Instantiate(image.material); // Clone material

        image.material.SetVector("_ImageSize", new Vector4(imageRectTransform.rect.size.x, imageRectTransform.rect.size.y, 0, 0));

        //MaxHealthPoints = GetComponentInParent<CharacterStats>().maxHealth; // Force the call to the setter in order to update the material
        //currentHealthPoints = MaxHealthPoints; // Force the call to the setter in order to update the material
    }

    protected void Update()
    {
        if (Damages > 0)
        {
            Damages -= damagesDecreaseRate * Time.deltaTime;
        }
    }

    public void Hurt(float damagesPoints)
    {
        Damages = damagesPoints;
        Health -= Damages;
    }
}
