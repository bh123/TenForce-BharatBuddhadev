using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }

        /// <summary>
        /// Mean temperature of the planet itself in Kelvin (API field avgTemp).
        /// Used when every moon has an unknown temperature (API value 0).
        /// </summary>
        public float AvgTemp { get; set; }

        /// <summary>
        /// Average temperature of this planet's moons, in Kelvin.
        /// The bodies API stores 0 when a moon's temperature is unknown, so those
        /// moons are skipped. If no moon has a known temperature, the planet's own
        /// avgTemp is used so the list is not left at the original 0.00 stub.
        /// </summary>
        public float AverageMoonTemperature
        {
            get
            {
                if (!HasMoons())
                {
                    return AvgTemp;
                }

                var moonsWithKnownTemperature = Moons.Where(moon => moon.AvgTemp > 0).ToList();
                if (moonsWithKnownTemperature.Count == 0)
                {
                    return AvgTemp;
                }

                return moonsWithKnownTemperature.Average(moon => moon.AvgTemp);
            }
        }

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            AvgTemp = planetDto.AvgTemp;
            Moons = new Collection<Moon>();
            if(planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
            }
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
    }
}
