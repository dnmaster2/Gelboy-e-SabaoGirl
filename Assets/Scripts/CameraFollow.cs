using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Public Variables
    [Tooltip("Referencia do player")]
	public Transform target;
	[Tooltip("Velocidade da camera")]
	public float smoothSpeed = 0.125f;
	[Tooltip("Vector de ajuste da camera")]
	public Vector3 offset;
    #endregion

    #region MonoBehaviour Callbacks
    void Update()
	{
		//Calcula a posição da camera baseada no offset
		Vector3 desiredPosition = target.position + offset;
		//Realiza a interpolação entre a posição atual e o destino, na velocidade da variavel
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		//Atribui para o position da camera
		transform.position = smoothedPosition;
		//Ajusta a rotação para a direção do player
		transform.LookAt(target);
	}
    #endregion
}
