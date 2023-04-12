using System;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex2023.Models
{
	public class MummyData
	{
		public float squarenorthsouth { get; set; }
        public float depth { get; set; }
        public float westtohead { get; set; }
        public float westtofeet { get; set; }
        public float sex_M { get; set; }
        public float sex_Unknown { get; set; }
        public float eastwest_W { get; set; }
        public float adultsubadult_A { get; set; }
        public float adultsubadult_C { get; set; }
        public float ageatdeath_A { get; set; }
        public float ageatdeath_C { get; set; }
        public float ageatdeath_I { get; set; }
        public float ageatdeath_N { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {squarenorthsouth,
            depth,
            westtohead,
            westtofeet,
            sex_M,
            sex_Unknown,
            eastwest_W,
            adultsubadult_A,
            adultsubadult_C,
            ageatdeath_A,
            ageatdeath_C,
            ageatdeath_I,
            ageatdeath_N
            };
            int[] dimensions = new int[] { 1, 13 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}

