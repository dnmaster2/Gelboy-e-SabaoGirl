using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorControlScript : MonoBehaviour
{
    #region Custom Script Variables
    PlayerPathScript pathfinding;
    CombatScript combatScript;
    #endregion
    #region Unity Objects Variables
    Camera cam;
    [Tooltip("Referencia do cursor de movimento")]
    public GameObject cursor;
    [Tooltip("referencias do canhão")]
    public GameObject cannonShoot, cannonCursor;
    #endregion
    #region Public Variables
    [Tooltip("Distancia do fim do dash quando o combate é selecionado")]
    public float buffCombatTarget;
    GameObject target;
    public bool moving;
    public bool combat;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        //Load
        cam = Camera.main;
        pathfinding = GetComponent<PlayerPathScript>();
        combatScript = GetComponent<CombatScript>();
    }

    private void Update()
    {
        //Associa o cursor com a booleana de canhão ativo
        cannonCursor.SetActive(BuffManager.instance.cannonIsActive);
        if (Input.GetMouseButtonDown(0))
        {
            if (BuffManager.instance.cannonIsActive)
            {
                //Pega o clique e gira na direção dele, instancia uma bola de canhão
                FindObjectOfType<AudioManager>().Play("GelJump");
                var cannonLookPos = cannonCursor.transform.position - transform.position;
                cannonLookPos.y = 0;
                var cannonRotation = Quaternion.LookRotation(cannonLookPos);
                GameObject cannonBall = Instantiate(cannonShoot, transform.position + transform.forward * 2, cannonRotation, transform.parent);
            }
        }

        if (BuffManager.instance.cannonIsActive)
        {
            //Controle do cursor
            cannonCursor.SetActive(true);
            //Raycast definindo a posição do cursor
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    cannonCursor.transform.position = hit.point;
                }

            }
            //Girar o player na direção do cursor
            var cannonLookPos = cannonCursor.transform.position - transform.position;
            cannonLookPos.y = 0;
            var cannonRotation = Quaternion.LookRotation(cannonLookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, cannonRotation, Time.deltaTime * 10);
            //IMPORTANTE caso o player tenha ativado o canhão, o resto deve parar
            return;
        }

        //Rotação em Y do player.
        var lookPos = cursor.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        //Associação do cursor com a bool de moving
        cursor.SetActive(moving);

        //Primeira parte: Toque/clique segurado
        if (Input.GetMouseButton(0))
        {
            //Manter track
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                { 
                    if (hit.collider.CompareTag("Walkable"))
                    {
                        //Atualiza a bool de combate como falsa cada frame, em caso de desistencia de um ataque
                        cursor.transform.position = hit.point;
                        combat = false;
                    }
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        //Se for num inimigo, trocar a booleana para combat, isso vai ser importante no MouseUp
                        combat = true;
                        cursor.transform.position = hit.point;
                        //Salva uma referencia do target, usado no CombatScript
                        target = hit.collider.gameObject;
                    }
                }
            }
        }

        //Segunda parte: Soltou o toque/clique
        if (Input.GetMouseButtonUp(0))
        {
            //Basicamente checa qual das booleanas de controle estão ativas e desativadas
            if (!combat)
            {
                //Movimento
                pathfinding.NewPath(cursor.transform.position);
                moving = false;
                return;
            }
            else
            {
                //Apenas pra evitar bug
                if (target)
                {
                    //Chama a função validando a proxima colisão com a bool StartCombat
                    if (!combatScript.startCombat)
                    {
                        combatScript.startCombat = true;
                    }
                    //Define o caminho para o inimigo + buff para que o player não pare na frente do inimigo
                    pathfinding.NewPath(target.transform.position + transform.forward * buffCombatTarget);

                    moving = false;
                }
                return;
            }
        }
    }
    #endregion
}
