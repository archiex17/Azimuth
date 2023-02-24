namespace Azimuth.GameObjects
{
	public static class GameObjectManager
	{
		private static List<GameObject> gameObjects = new List<GameObject>();

		public static void Spawn(GameObject _gameObject)
		{
			if(!gameObjects.Contains(_gameObject))
			{
				gameObjects.Add(_gameObject);
				_gameObject.Load();
			}
		}

		public static void Destroy(GameObject _gameObject)
		{
			if(gameObjects.Contains(_gameObject))
			{
				_gameObject.Unload();
				gameObjects.Remove(_gameObject);
			}
		}

		public static void Update(float _deltaTime)
		{
			foreach(GameObject gameObject in gameObjects)
				gameObject.Update(_deltaTime);
		}

		public static void Draw()
		{
			foreach(GameObject gameObject in gameObjects)
				gameObject.Draw();
		}
	}
}