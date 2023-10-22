using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {

    private int hp;
    [SerializeField] private GameObject destroyParticleSystem;
    [SerializeField] private Color[] colorArr;
    [SerializeField] private Color damageColor;
    private Color baseColor;

    private LineRenderer lr;
    private EdgeCollider2D coll;
    public event System.Action<Wire> OnWireCut;

    public Coils coils { get; private set; }
    public float Distance => Vector2.Distance(coils.coil1.position, coils.coil2.position); 

    void Awake() {
        lr = GetComponent<LineRenderer>();
        coll = GetComponent<EdgeCollider2D>();
        baseColor = colorArr[Random.Range(0, colorArr.Length)];
        lr.startColor = baseColor;
        lr.endColor = baseColor;
    }

    void OnDestroy() {
        Web.Instance.UnregisterWire(this);
        OnWireCut?.Invoke(this);
    }

    public void Init(Coil coil1, Coil coil2) {
        coils = new Coils(coil1, coil2);
        lr.SetPositions(new Vector3[] { coil1.position, coil2.position } );

        SetUpCollider();
        Web.Instance.RegisterWire(this);
    }

    private void SetUpCollider() {
        List<Vector2> edges = new List<Vector2>();

        for (int i = 0; i < lr.positionCount; i++) {
            Vector2 lrPos = lr.GetPosition(i) - transform.position;
            edges.Add(new Vector2(lrPos.x, lrPos.y));
        } coll.SetPoints(edges);
    }

    public void SimulateDamage(int dmgVal) => StartCoroutine(_SimulateDamage(dmgVal));

    private IEnumerator _SimulateDamage(int dmgVal) {
        Color color = lr.startColor;
        while (color != damageColor) {
            color = LerpLRColor(color, damageColor);
            yield return null;
        } hp -= dmgVal;
        if (hp <= 0) {
            GameObject dps = Instantiate(destroyParticleSystem);
            dps.GetComponent<WireDestroyPS>().Init(lr);
            Destroy(gameObject);
        } else {
            while (color != baseColor) {
                color = LerpLRColor(color, baseColor);
                yield return null;
            }
        }
    }

    private Color LerpLRColor(Color color, Color targetColor) {
        color = Vector4.MoveTowards(color, targetColor, Time.deltaTime * 2f);
        lr.startColor = color;
        lr.endColor = color;
        return color;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Boss>()) SimulateDamage(3);
    }
}

public struct Coils {
    public Coil coil1;
    public Coil coil2;

    public Coils(Coil coil1, Coil coil2) {
        this.coil1 = coil1;
        this.coil2 = coil2;
    }
}
