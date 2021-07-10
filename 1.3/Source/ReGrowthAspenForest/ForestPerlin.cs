using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Noise;

namespace ReGrowthAspenForest
{
	[StaticConstructorOnStartup]
	public static class ForestPerlin
	{

		static ForestPerlin()
		{
			SetupForrestNoise();
		}
		private static float FreqMultiplier
		{
			get
			{
				return 1f;
			}
		}

		private static void SetupForrestNoise()
		{

			float freqMultiplier = FreqMultiplier;
			ModuleBase moduleBase = new Perlin((double)(0.09f * freqMultiplier), 2.0, 0.40000000596046448, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
			ModuleBase moduleBase2 = new RidgedMultifractal((double)(0.025f * freqMultiplier), 2.0, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
			moduleBase = new ScaleBias(0.5, 0.5, moduleBase);
			moduleBase2 = new ScaleBias(0.5, 0.5, moduleBase2);
			noiseElevation = new Multiply(moduleBase, moduleBase2);
			InverseLerp inverseLerp = new InverseLerp(noiseElevation, ElevationRange.max, ElevationRange.min);
			noiseElevation = new Multiply(noiseElevation, inverseLerp);
			NoiseDebugUI.StorePlanetNoise(noiseElevation, "noiseRadiation");
		}

		[Unsaved(false)]
		public static ModuleBase noiseElevation;

		private static readonly FloatRange ElevationRange = new FloatRange(650f, 750f);
	}
}
