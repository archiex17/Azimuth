namespace Azimuth.GameStates
{
	public interface IGameState
	{
		// ReSharper disable once InconsistentNaming
		public string ID { get; }

		public void Load();
		public void Update(float _deltaTime);
		public void Draw();
		public void Unload();
	}
}