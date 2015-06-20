using System;
using SFML.Graphics;

namespace ZTD {

	class PerlinNoise {

		public int Width { get; set; }
		public int Height { get; set; }
		public int Seed { get; set; }

		public PerlinNoise(int seed) {
			Seed = seed;
		}

		private double[,] GenerateSimpleNoise(int width, int height) {
			Width = width;
			Height = height;

			double[,] noise = new double[Width, Height];

			Random random = new Random(Seed);

			for(int x = 0; x < Width; x++)
				for(int y = 0; y < Height; y++)
					noise[x, y] = random.NextDouble();

			return noise;
		}


		public double[,] GenerateNoise(int width, int height) {
			double[,] noise = GenerateSimpleNoise(width, height);

			double[,] result = new double[width, height];

			for(int x = 0; x < Width; x++)
				for(int y = 0; y < Height; y++)
					result[x, y] = (byte)(Turbulence(x / 2.0, y / 2.0, 64, noise));

			return result;
		}

		public Image GetNoiseImage(int width, int height) {
			double[,] noise = GenerateNoise(width, height);

			Color[,] colors = new Color[Width, Height];

			for(int x = 0; x < Width; x++)
				for(int y = 0; y < Height; y++) {
					double val = noise[x, y];
					colors[x, y] = new Color((byte)(val * 0.6), (byte)val, (byte)(val * 0.6));
				}

			return new Image(colors);
		}

		double SmoothNoise(double x, double y, double[,] noise) {
			//get fractional part of x and y
			double fractX = x - (int)x;
			double fractY = y - (int)y;

			//wrap around
			int x1 = ((int)x + Width) % Width;
			int y1 = ((int)y + Height) % Height;

			//neighbor values
			int x2 = (x1 + Width - 1) % Width;
			int y2 = (y1 + Height - 1) % Height;

			//smooth the noise with bilinear interpolation
			double value = 0;
			value += fractX * fractY * noise[x1, y1];
			value += fractX * (1 - fractY) * noise[x1, y2];
			value += (1 - fractX) * fractY * noise[x2, y1];
			value += (1 - fractX) * (1 - fractY) * noise[x2, y2];

			return value;
		}

		double Turbulence(double x, double y, double size, double[,] noise) {
			double value = 0;
			double initialSize = size;

			while(size >= 1) {
				value += SmoothNoise(x / size, y / size, noise) * size;
				size /= 2.0;
			}

			return (128.0 * value / initialSize);
		}
	}
}