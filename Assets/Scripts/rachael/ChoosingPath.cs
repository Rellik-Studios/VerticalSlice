using UnityEngine;
using UnityEngine.Serialization;

namespace rachael
{
    public class ChoosingPath : MonoBehaviour
    {
        public GameObject m_player;
        [FormerlySerializedAs("assignRoom")] public GameObject m_assignRoom;
        [FormerlySerializedAs("assignDoor")] public GameObject m_assignDoor;
        [FormerlySerializedAs("eraChanging")] public ChangeFurniture m_eraChanging;
        [FormerlySerializedAs("assignedName")] public string m_assignedName;

        [FormerlySerializedAs("route")] [SerializeField] private int m_route;
        // Start is called before the first frame update
        void Awake()
        {
      
            if (PlayerPrefs.HasKey(m_assignedName))
            {
                //
                //PlayerPrefs.SetInt(assignedName, 1);
                m_route = PlayerPrefs.GetInt(m_assignedName);
                m_eraChanging.m_rooms[m_route] = m_assignRoom;
                m_assignDoor.GetComponent<DoorToWall>().TransformDoorToWall();

                Destroy(this);
            }
        
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider _other)
        {
            Debug.Log("Contact");
            if (_other.CompareTag("Player"))
            {
                if (m_eraChanging != null)
                {
                    m_route = m_eraChanging.index;
                    m_eraChanging.m_rooms[m_route] = m_assignRoom;
                    //PlayerPrefs.SetInt(assignedName, route);
                    m_assignDoor.GetComponent<DoorToWall>().FadeHubWall();
                    Destroy(this);
                }
            }
        }
    }
}
