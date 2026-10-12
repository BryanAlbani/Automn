using UnityEngine;
using UnityEngine.InputSystem;

public class LeafCollectorBarrel : Singleton<LeafCollectorBarrel>
{
    [SerializeField] private string targetTag = "FallingObject";
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform flame;

    [Header("Barrel Parameters")]
    public float SpeedBarrel = 1f;
    public float ScaleBarrel = 1f;

    private Vector3 initialScaleBarrel;
    private Vector3 initialScaleFlame;

    private Vector3 initialPosBarrel;
    private Vector3 initialPosFlame;

    protected override void Awake()
    {
        base.Awake();

        initialScaleBarrel = barrel.localScale;
        initialScaleFlame = flame.localScale;

        initialPosBarrel = barrel.localPosition;
        initialPosFlame = flame.localPosition;
    }

    void Start()
    {
        SpeedBarrel = DataManager.Instance.SpeedBarrel;
        ScaleBarrel = DataManager.Instance.ScaleBarrel;
        ScaleBarrelFunc();
    }

    void Update()
    {
        HandleMovementInput();
    }


    private void HandleMovementInput()
    {
        float horizontal = 0f;

        if (Keyboard.current == null)
            return;

        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;
        }

        transform.position += Vector3.right * horizontal * SpeedBarrel * Time.deltaTime;
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            ItemsFall leaf = other.GetComponent<ItemsFall>();
            UpgradeManager.Instance.UpdateMoney(leaf.valueHeat);
            // Play sound burn
            Destroy(other.gameObject);

            // Logique increment money
        }
    }

    private void ScaleBarrelFunc()
    {
        SpriteRenderer barrelRenderer = barrel.GetComponent<SpriteRenderer>();

        float bottomBefore = barrelRenderer.bounds.min.y;

        barrel.localScale = initialScaleBarrel * ScaleBarrel;
        flame.localScale = initialScaleFlame * ScaleBarrel;

        barrel.localPosition = new Vector3(initialPosBarrel.x * ScaleBarrel, initialPosBarrel.y * ScaleBarrel, initialPosBarrel.z);

        flame.localPosition = new Vector3(initialPosFlame.x * ScaleBarrel, initialPosFlame.y * ScaleBarrel, initialPosFlame.z);

        float bottomAfter = barrelRenderer.bounds.min.y;
        float offsetY = bottomBefore - bottomAfter;

        barrel.position += Vector3.up * offsetY;
        flame.position += Vector3.up * offsetY;
    }
}
