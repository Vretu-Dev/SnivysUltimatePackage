using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using VVUP.CustomRoles.Roles.Chaos;
using VVUP.CustomRoles.Roles.ClassD;
using VVUP.CustomRoles.Roles.Foundation;
using VVUP.CustomRoles.Roles.Other;
using VVUP.CustomRoles.Roles.Scientist;
using VVUP.CustomRoles.Roles.Scps;

namespace VVUP.CustomRoles
{
    public class CustomRolesConfig : IConfig
    {
        [Description("Enables Custom Roles")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
        
        public static List<ContainmentScientist> ContainmentScientists { get; set; } = new()
        {
            new ContainmentScientist(),
        };
        public static List<LightGuard> LightGuards { get; set; } = new()
        {
            new LightGuard(),
        };
        public static List<Biochemist> Biochemists { get; set; } = new()
        {
            new Biochemist(),
        };
        public static List<ContainmentGuard> ContainmentGuards { get; set; } = new()
        {
            new ContainmentGuard(),
        };
        public static List<BorderPatrol> BorderPatrols { get; set; } = new()
        {
            new BorderPatrol(),
        };
        public static List<Nightfall> Nightfalls { get; set; } = new()
        {
            new Nightfall(),
        };
        public static List<A7Chaos> A7Chaoss { get; set; } = new()
        {
            new A7Chaos(),
        };
        public static List<Flipped> Flippeds { get; set; } = new()
        {
            new Flipped(),
        };
        public static List<TelepathicChaos> TelepathicChaos { get; set; } = new()
        {
            new TelepathicChaos(),
        };
        public static List<JuggernautChaos> JuggernautChaos { get; set; } = new()
        {
            new JuggernautChaos(),
        };
        public static List<CISpy> CISpies { get; set; } = new()
        {
            new CISpy(),
        };
        public static List<MtfWisp> MtfWisps { get; set; } = new()
        {
            new MtfWisp(),
        };
        public static List<DwarfZombie> DwarfZombies { get; set; } = new()
        {
            new DwarfZombie(),
        };
        public static List<ExplosiveZombie> ExplosiveZombies { get; set; } = new()
        {
            new ExplosiveZombie(),
        };
        public static List<CiPhantom> CiPhantoms { get; set; } = new()
        {
            new CiPhantom(),
        };

        public static List<MedicZombie> MedicZombies { get; set; } = new()
        {
            new MedicZombie(),
        };

        public static List<LockpickingClassD> LockpickingClassDs { get; set; } = new()
        {
            new LockpickingClassD(),
        };

        public static List<Demolitionist> Demolitionists { get; set; } = new()
        {
            new Demolitionist(),
        };
        
        public static List<Vanguard> Vanguards { get; set; } = new()
        {
            new Vanguard(),
        };

        public static List<TheoreticalPhysicistScientist> TheoreticalPhysicistScientists { get; set; } = new()
        {
            new TheoreticalPhysicistScientist(),
        };
        
        public static List<MtfParamedic> MtfParamedics { get; set; } = new()
        {
            new MtfParamedic(),
        };
        
        public static List<ClassDAnalyst> ClassDAnalysts { get; set; } = new()
        {
            new ClassDAnalyst(),
        };
        
        public static List<ClassDTank> ClassDTanks { get; set; } = new()
        {
            new ClassDTank(),
        };
        
        public static List<InfectedZombie> InfectedZombies { get; set; } = new()
        {
            new InfectedZombie(),
        };
        
        public static List<PoisonousZombie> PoisonousZombies { get; set; } = new()
        {
            new PoisonousZombie(),
        };

        public static List<SpeedsterZombie> SpeedsterZombies { get; set; } = new()
        {
            new SpeedsterZombie(),
        };

        public static List<TeleportZombie> TeleportZombies { get; set; } = new()
        {
            new TeleportZombie()
        };
    }
}