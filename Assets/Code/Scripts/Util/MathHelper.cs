using System;

namespace Code.Scripts.Util {
	public interface MathHelper {
		public static int lerp(int a, int b, double t) {
			return (int) Math.Round(a + (b - a) * t);
		}

		public static float lerp(float a, float b, double t) {
			return (float) (a + (b - a) * t);
		}

		public static double lerp(double a, double b, double t) {
			return a + (b - a) * t;
		}

		public static int largest(int a, int b) {
			var absA = Math.Abs(a);
			var absB = Math.Abs(b);
			return absA > absB ? a : b;
		}

		public static float largest(float a, float b) {
			var absA = Math.Abs(a);
			var absB = Math.Abs(b);
			return absA > absB ? a : b;
		}

		public static double largest(double a, double b) {
			var absA = Math.Abs(a);
			var absB = Math.Abs(b);
			return absA > absB ? a : b;
		}

		public static int clamp(int value, int min, int max) {
			return Math.Min(Math.Max(value, min), max);
		}

		public static float clamp(float value, float min, float max) {
			return Math.Min(Math.Max(value, min), max);
		}

		public static double clamp(double value, double min, double max) {
			return Math.Min(Math.Max(value, min), max);
		}

		public static int clampLoop(int value, int min, int max) {
			if (value < min) {
				return max - (min - value) % (max - min);
			}
			return min + (value - min) % (max - min);
		}

		public static float clampLoop(float value, float min, float max) {
			if (value < min) {
				return max - (min - value) % (max - min);
			}
			return min + (value - min) % (max - min);
		}

		public static double clampLoop(double value, double min, double max) {
			if (value < min) {
				return max - (min - value) % (max - min);
			}
			return min + (value - min) % (max - min);
		}

		public static bool getByteFlag(byte data, int flag) {
			if (flag is < 0 or > 8) {
				throw new ArgumentException("Invalid byte flag index: " + flag);
			}
			return (data >> flag & 0x01) == 1;
		}

		public static byte setByteFlag(byte data, int flag, bool value) {
			if (flag is < 0 or > 8) {
				throw new ArgumentException("Invalid byte flag index: " + flag);
			}
			if (value) {
				return (byte) (data | 1 << flag);
			}
			return (byte) (data & ~(1 << flag));
		}
	}
}