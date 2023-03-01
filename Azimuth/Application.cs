using Azimuth.GameObjects;
using Azimuth.GameStates;

using Raylib_cs;

namespace Azimuth
{
	public sealed class Application
	{
		public static Application? Instance { get; private set; }

		public static void Run<GAME>() where GAME : Game, new()
		{
			if(Instance != null)
			{
				Console.WriteLine("[Error] Attempted to run application more than once!");

				return;
			}

			Instance = new Application(new GAME());
			Instance.Run();
		}
		
		public Window Window { get; }

		private readonly Game game;

		private Application(Game _game)
		{
			Config.Create();
			Window = new Window();
			game = _game;
		}

		private void Run()
		{
			Window.Open();
			Assets.Load();
			
			game.Load();

			while(!Raylib.WindowShouldClose())
			{
				float deltaTime = Raylib.GetFrameTime();
				GameObjectManager.Update(deltaTime);
				GameStateManager.Update(deltaTime);
				
				Raylib.BeginDrawing();
				Window.Clear();
				
				// Global drawing here
				GameObjectManager.Draw();
				GameStateManager.Draw();
				
				Raylib.EndDrawing();
			}
			
			game.Unload();
			Assets.Unload();
			
			Window.Close();
		}
	}
}