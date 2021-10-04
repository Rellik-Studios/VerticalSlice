using UnityEngine;

namespace Himanshu
{
    public class CollectableObject : MonoBehaviour, IInteract
    {
        
        public void Execute(PlayerInteract _player)
        {
            Debug.Log("Collect");
            GetComponent<AudioSource>().Play();
            _player.Collect();
            GetComponent<MeshCollider>().enabled = false;
            
            this.Invoke(()=> { Destroy(this.gameObject); }, 0.1f);
        }
    }
}