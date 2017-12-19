using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Resources.src
{
	class Utils
	{
		public static Texture2D LoadTexture(string FilePath)
		{

			// Load a PNG or JPG file from disk to a Texture2D
			// Returns null if load fails

			Texture2D Tex2D;
			byte[] FileData;

			if (File.Exists(FilePath))
			{
				FileData = File.ReadAllBytes(FilePath);
				Tex2D = new Texture2D(2, 2);		// Create new "empty" texture
				if (Tex2D.LoadImage(FileData))		// Load the imagedata into the texture (size is set automatically)
					return Tex2D;					// If data = readable -> return texture
			}
			return null;							// Return null if load failed
		}
	}
}
