using UnityEngine;
using UnityEngine.InputSystem;

public class LeafCollectorBarrel : Singleton<LeafCollectorBarrel>
{
    [SerializeField] private string targetTag = "FallingObject";
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform flame;

    [SerializeField] private BoxCollider2D barrelCollider;

    private Vector2 initialColliderSize;
    private Vector2 initialColliderOffset;

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
        initialColliderSize = barrelCollider.size;
        initialColliderOffset = barrelCollider.offset;
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
            AudioManager.Instance.PlaySFX(AudioManager.Instance.Burn);
            ItemsFall leaf = other.GetComponent<ItemsFall>();
            UpgradeManager.Instance.UpdateMoney(leaf.valueHeat);
            Destroy(other.gameObject);

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

        UpdateBarrelCollider();
    }


    private void UpdateBarrelCollider()
    {
        SpriteRenderer barrelRenderer = barrel.GetComponent<SpriteRenderer>();

        if (barrelRenderer == null || barrelCollider == null)
            return;

        Bounds bounds = barrelRenderer.bounds;

        Vector3 localCenter = transform.InverseTransformPoint(bounds.center);

        barrelCollider.size = new Vector2(bounds.size.x / Mathf.Abs(transform.lossyScale.x),bounds.size.y / Mathf.Abs(transform.lossyScale.y));

        barrelCollider.offset = new Vector2(localCenter.x,localCenter.y + 0.5f);
    }

}
