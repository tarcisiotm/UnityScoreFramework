using UnityEngine;

namespace Score
{
	public abstract class Singleton<T> : MonoBehaviour where T : Component
	{
		protected static T m_instance = null;

		public static T Instance
		{
			get
			{
				if (m_instance != null)
				{
					return m_instance;
				}

				return InstantiateObject(null);
			}
		}

		/// <summary>
		/// For now, it needs to have the object already under GameManager for performance reasons to avoid instantiating stuff during runtime.
		/// </summary>
		/// <returns>The object.</returns>
		/// <param name="owner">Owner.</param>
		static T InstantiateObject(GameObject owner)
		{
			if (m_instance == null)
			{
				if (owner != null)
				{
					m_instance = owner.GetComponent<T> ();
				}
				else
				{
					GameObject manager = GameObject.Find("GameManager");
					if (manager == null)
					{
						//manager = new GameObject ("GameManager");
						return null;
					}
					m_instance = manager.GetComponentInChildren<T>();
				}

				if (m_instance == null) {
					return null;
				}

				(m_instance as Singleton<T>).Init();

				DontDestroyOnLoad(m_instance.gameObject);
				//GameObjectManager.DontDestroyOnLoad(m_instance.gameObject);
			}

			return m_instance;
		}

		protected virtual void Init()
		{

		}

		public virtual void Awake()
		{
			if (m_instance != null && m_instance != this)
			{
				Destroy (gameObject);
				//GameObjectManager.Destroy(gameObject);
				return;
			}

			InstantiateObject(gameObject);

			DontDestroyOnLoad (gameObject);
			//GameObjectManager.DontDestroyOnLoad(gameObject);
		}

		public virtual void OnDisable()
		{
			if (m_instance == this)
			{
				m_instance = null;
			}
		}
	}
}