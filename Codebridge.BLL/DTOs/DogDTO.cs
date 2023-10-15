using System.ComponentModel.DataAnnotations;

namespace Codebridge.Data;

   public class DogDto 
    {
		public string Name { get; set; }


		public string Colour { get; set; }

		public int Tail_Lenght { get; set; }
		public int Weight { get; set; }
	}
