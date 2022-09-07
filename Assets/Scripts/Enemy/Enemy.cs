using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    #region Editor Variables
    [SerializeField] [Tooltip("The bounds of the enemy's possible spawn points.")]
    private Vector2 m_Bounds;

    [SerializeField] [Tooltip("The radius determines how far the enemy will move back and forth.")]
    private float m_Radius;
    #endregion

    #region Private Variables
    private float p_ElapsedTime;
    private Vector2 p_StartingPosition;
    #endregion


    // Start is called before the first frame update
    void Start() {
        float horizontalBound = m_Bounds.x / 2;
        float verticalBound = m_Bounds.y / 2;
        p_StartingPosition = new Vector2(
            Random.Range(-horizontalBound, horizontalBound),
            Random.Range(-verticalBound, verticalBound)
        );
        transform.position = p_StartingPosition;
        p_ElapsedTime = 0;
    }

    // Update is called once per frame
    void Update() {
        // Add randomness later if you want, but it'll be all over the place unless time is more closely managed
        transform.position = p_StartingPosition + new Vector2(m_Radius * Mathf.Sin(p_ElapsedTime),
            m_Radius * Mathf.Sin(p_ElapsedTime));

        p_ElapsedTime += Time.deltaTime;
    }
}