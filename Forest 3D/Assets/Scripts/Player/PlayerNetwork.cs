using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<PlayerNetworkData> netPos = new(writePerm: NetworkVariableWritePermission.Owner);
    private Vector3 val;
    public float cheapInterpolationTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        if(IsOwner)
        {
            netPos.Value = new PlayerNetworkData(){
                Position = transform.position
            };
        }else
        {
            transform.position = Vector3.SmoothDamp(transform.position, netPos.Value.Position , ref val , cheapInterpolationTime );
        }
    }

    struct PlayerNetworkData : INetworkSerializable {
        private float x, y, z;

        internal Vector3 Position {
            get => new Vector3 (x,y,z);
            set {
                x = value.x;
                y = value.y;
                z = value.z;
            }
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter{
            serializer.SerializeValue(ref x);
            serializer.SerializeValue(ref y);
            serializer.SerializeValue(ref z);
        }
    }
}
