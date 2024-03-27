using System.Linq;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] BaseEffect[] allEffect;

    public static EffectManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public GameObject GetEffect<T>() where T : BaseEffect
    {
        return allEffect.Where(e => typeof(T).IsAssignableFrom(e.GetType())).FirstOrDefault().gameObject;
    }
}
