using Raylib_cs;

namespace Azimuth
{
	public static class Assets
	{
		private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
		private static Dictionary<string, Image> images = new Dictionary<string, Image>();
		private static Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();
		private static Dictionary<string, Font> fonts = new Dictionary<string, Font>();

		public static ASSET_TYPE Find<ASSET_TYPE>(string _id)
		{
			if(_id.Contains("Textures") && textures.ContainsKey(_id))
				return (ASSET_TYPE) Convert.ChangeType(textures[_id], typeof(ASSET_TYPE));
			
			if(_id.Contains("Images") && images.ContainsKey(_id))
				return (ASSET_TYPE) Convert.ChangeType(images[_id], typeof(ASSET_TYPE));
			
			if(_id.Contains("Sounds") && sounds.ContainsKey(_id))
				return (ASSET_TYPE) Convert.ChangeType(sounds[_id], typeof(ASSET_TYPE));
			
			if(_id.Contains("Fonts") && fonts.ContainsKey(_id))
				return (ASSET_TYPE) Convert.ChangeType(fonts[_id], typeof(ASSET_TYPE));
			
			throw new FileNotFoundException($"Asset with ID '{_id}' does not exist!");
		}

	#region Loading Assets

		public static void Load()
		{
			LoadAllOfType<Texture2D>(textures, "Textures", "png", Raylib.LoadTexture);
			LoadAllOfType<Image>(images, "Images", "jpg", Raylib.LoadImage);
			LoadAllOfType<Sound>(sounds, "Sounds", "mp3", Raylib.LoadSound);
			LoadAllOfType<Font>(fonts, "Fonts", "ttf", Raylib.LoadFont);
		}

		private static void LoadAllOfType<ASSET_TYPE>(Dictionary<string, ASSET_TYPE> _assets, string _folder, string _extension, Func<string, ASSET_TYPE> _loadFnc)
		{
			List<string> files = LocateFiles(_folder, _extension);

			foreach(string file in files)
			{
				// Generate an id using the folder (asset type) and its local directory within the assets folder using some wizardry
				string id = string.Concat($"{_folder}/", file.AsSpan(file.LastIndexOf(_folder, StringComparison.Ordinal) + _folder.Length + 1));
				id = id.Replace($".{_extension}", "").Replace('\\', '/');
				
				_assets.Add(id, _loadFnc(file));
			}
		}

		private static List<string> LocateFiles(string _folder, string _extension)
		{
			List<string> files = new List<string>();

			string path = $"{Directory.GetCurrentDirectory()}\\{Path.Combine("Assets\\", _folder)}";

			// If the directory doesn't exist, we will ignore it.
			if(!Directory.Exists(path))
				return files;

			// Get all files within the directory that match the extension, considering sub folders
			foreach(string file in Directory.GetFiles(path, $"*.{_extension}", SearchOption.AllDirectories))
				files.Add(file);

			return files;
		}

	#endregion

	#region Unloading Assets

		public static void Unload()
		{
			ClearMemoryFor<Texture2D>(textures, Raylib.UnloadTexture);
			ClearMemoryFor<Image>(images, Raylib.UnloadImage);
			ClearMemoryFor<Sound>(sounds, Raylib.UnloadSound);
			ClearMemoryFor<Font>(fonts, Raylib.UnloadFont);
		}

		private static void ClearMemoryFor<ASSET_TYPE>(Dictionary<string, ASSET_TYPE> _assets, Action<ASSET_TYPE> _unloadFnc)
		{
			foreach(ASSET_TYPE asset in _assets.Values)
				_unloadFnc(asset);
			
			_assets.Clear();
		}

	#endregion
	}
}