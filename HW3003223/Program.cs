using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3003223
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<AirTransport> airTransports = new List<AirTransport>();
			List<LandingStrip> landingStrips = new List<LandingStrip>();

			AirTrafficControl Dispatcher = new AirTrafficControl(ref airTransports, ref landingStrips);

			landingStrips.Add(new LandingStrip("A"));
			landingStrips.Add(new LandingStrip("B"));

			airTransports.Add(new AirTransport("Cobra", Dispatcher));
			airTransports.Add(new AirTransport("Dragon", Dispatcher));
			airTransports.Add(new AirTransport("Daniel", Dispatcher));

			airTransports[0].Landing();
			airTransports[1].Landing();
			airTransports[2].Landing();

			Console.ReadKey(true);
		}

		public class AirTransport
		{
			public string Name { get; set; }

			public bool is_landed { get; set; }

			public AirTrafficControl Dispatcher { get; set; }

			public AirTransport(string name, AirTrafficControl dispatcher)
			{
				Name = name;
				Dispatcher = dispatcher;
				is_landed = false;
			}

			public void Landing() 
			{
				Dispatcher.SendMessage(this);
			}

			public override string ToString()
			{
				return Name;
			}
		}

		public class AirTrafficControl
		{
			public List<LandingStrip> LandingStrips { get; set; }
			public List<AirTransport> AirTransports { get; set; }

			public AirTrafficControl(ref List<AirTransport> airTransports, ref List<LandingStrip> landingStrips)
			{
				AirTransports = airTransports;
				LandingStrips = landingStrips;
			}

			public void SendMessage(AirTransport transport)
			{
				foreach (LandingStrip strip in LandingStrips) {
					if (!strip.is_occupied) {
						strip.is_occupied = true;
						transport.is_landed = true;
						Console.WriteLine($"Plane { transport } lands on runway { strip }");
						return;
					}
				}
				Console.WriteLine($"At the moment there is no free lane. Airplane { transport } must circle around the airport");
			}
		}

		public class LandingStrip
		{
			public string Name { get; set; }
			public bool is_occupied { get; set; }

			public LandingStrip(string name)
			{
				is_occupied = false;
				Name = name;
			}

			public override string ToString()
			{
				return Name;
			}
		}
	}
}
