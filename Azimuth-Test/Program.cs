using Azimuth;

using Raylib_cs;

namespace Azimuth_Test
{
	public static class Program
	{
		public static void Main()
		{
			Application.Run(800, 600, "Azimuth Test", Color.WHITE, new AzimuthTestGame());
		}
	}
}