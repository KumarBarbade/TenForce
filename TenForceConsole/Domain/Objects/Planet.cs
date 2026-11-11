using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TenForceConsole.Domain.DataTransferObjects;

namespace TenForceConsole.Domain.Objects
{
    public class Planet
    {
        public string? Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }
        public float AverageMoonGravity
        {
            get => 0.0f;
        }

        public float AverageMoonTemperature => Moons == null || Moons.Count == 0 ? 0f : Moons.Average(m => m.Temperature);

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
