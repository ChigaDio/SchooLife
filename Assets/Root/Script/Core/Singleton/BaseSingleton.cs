using UnityEngine;

namespace GameCore
{
    public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // �܂��A���ɃV�[�����ɂ��邩�`�F�b�N
                    instance = GameObject.FindFirstObjectByType<T>();

                    if (instance == null)
                    {
                        // �܂��Ȃ���ΐV��������
                        GameObject instanceObj = new GameObject();
                        instance = instanceObj.AddComponent<T>();
                        instanceObj.name = typeof(T).Name;
                    }
                }

                return instance;
            }
        }

        public virtual void AwakeSingleton()
        {
            if (instance == null)
            {
                instance = gameObject.GetComponent<T>();
            }
        }

        public void Awake()
        {
            AwakeSingleton();
        }
    }
}
