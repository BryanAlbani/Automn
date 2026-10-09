using UnityEngine;

public class ItemsFall : Singleton<ItemsFall>
{
    public string name;
   
    [Header("Chute")]
    [SerializeField] private float minFallSpeed = 0.5f;
    [SerializeField] private float maxFallSpeed = 1.5f;

    [Header("Balancement")]
    [SerializeField] private float swayAmplitude = 0.5f;
    [SerializeField] private float swayFrequency = 2f;

    [Header("Rotation")]
    [SerializeField] private float minRotationSpeed = -30f;
    [SerializeField] private float maxRotationSpeed = 30f;

    private float _fallSpeed;
    private float _rotationSpeed;
    private float _timeOffset;
    private float _previousSway;

    private void Start()
    {
        _fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);

        _rotationSpeed = Random.Range(minRotationSpeed,maxRotationSpeed);

        _timeOffset = Random.Range(0f, Mathf.PI * 2f);
        _previousSway = Mathf.Sin(_timeOffset) * swayAmplitude;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        transform.position += Vector3.down * _fallSpeed * deltaTime;

        float sway = Mathf.Sin(Time.time * swayFrequency + _timeOffset) * swayAmplitude;

        transform.position += Vector3.right * (sway - _previousSway);
        _previousSway = sway;


        transform.Rotate(0f,0f,_rotationSpeed * deltaTime);
    }
}

