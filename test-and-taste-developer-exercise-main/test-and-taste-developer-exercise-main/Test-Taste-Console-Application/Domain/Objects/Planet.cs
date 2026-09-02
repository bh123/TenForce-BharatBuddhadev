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
        /// Average temperature of this planet's moons, in Kelvin.
        /// The API often stores 0 when temperature is unknown, so those moons are skipped.
        /// </summary>
        public float AverageMoonTemperature
        {
            get
            {
                if (!HasMoons())
                {
                    return 0.0f;
                }

                var moonsWithKnownTemperature = Moons.Where(moon => moon.AvgTemp > 0).ToList();
                if (moonsWithKnownTemperature.Count == 0)
                {
                    return 0.0f;
                }

                return moonsWithKnownTemperature.Average(moon => moon.AvgTemp);
            }
        }

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
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
