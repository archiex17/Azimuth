using Raylib_cs;

namespace Azimuth
{
	public sealed class Window
	{
		public int Width { get; }
		public int Height { get; }
		public string Title { get; }
		public Color ClearColor { get; }

		public Window(int _width, int _height, string _title, Color _clearColor)
		{
			Width = _width;
			Height = _height;
			Title = _title;
			ClearColor = _clearColor;
		}

		public void Open()
		{
			Raylib.InitWindow(Width, Height, Title);
		}

		public void Clear()
		{
			Raylib.ClearBackground(ClearColor);
		}

		public void Close()
		{
			Raylib.CloseWindow();
		}
	}
}