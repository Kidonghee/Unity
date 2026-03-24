using UnityEngine;

public class FlashHitEffect : MonoBehaviour
{
    public float duration = 0.2f;
    public float growSpeed = 3f;

    private SpriteRenderer sr;
    private Color startColor;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 점점 커지기
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;

        // 점점 투명해지기
        float alpha = Mathf.Lerp(1f, 0f, timer / duration);
        sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}