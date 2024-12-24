Snivy's Ultimate Package contains all of the plugins I have made into one mega plugin for SCP SL.

I do mean made, any other plugins that I have ported or currently maintain, such as Serpents Hand, UIU Rescue Squad, etc is not included here.

Plugin List:
Snivy's Custom Roles
Snivy's Custom Roles Abilities
Snivy's Server Events
Micro Damage Reduction

New to this plugin is also
Snivy's Custom Items
Surface Final Escape Door Opener
Flamingo Adjustments
Micro Evaportate Players
SCP 1576 Spectator Viewer
Voting Commands

# Snivy's Custom Roles
### Role List
Below is a table of all the current custom roles, followed by a breif description of them. Many of them rely on special abilities also added by this plugin, refer to the list of abilities for more details about what each does.

RoleName | RoleID | Abilities | Spawn Type | Description
:---: | :---: | :---: | :---: | :------
Containment Engineer | 30 | Restricted Escape | Immediately when a round begins | A Scientist is randomly selected and is set into Enterance Zone with a Containment Engineer Keycard.
Protocol Enforcer | 31 | None | Immediately when a round begins | A lighter facility guard that spawns in light containment zone. They spawn with a Tranquilizer, Medkit, Painkillers, Radio, Light Armor, and a Zone Manager Keycard.
Biochemist| 32 | Healing Mist, Martyrdom, CustomRoleEscape | Immediately when a round begins | A Scientist genetically altered.
Containment Guard | 33 | None | Immediately when a round begins | A Facility Guard specializing in recontaining SCPs.
Border Patrol | 34 | None | Given by Admin Command only | A facility guard specialized in ensuring safe passage from Enterance and Heavy Checkpoints.
Nightfall | 35 | Data Missing | Data Missing | Data Missing.
A7 Chaos | 36 | None | During a Chaos Insurgency Respawn Wave | A Chaos Member that spawns with an A7.
Flipped | 37 | Flipped | Given by Admin Command only | For those people who complains about dwarfs when they spawn in as it.
Telepathic Chaos | 38 | Detect | During a Chaos Insurgency Respawn Wave | A Chaos Member that can detect hostiles to the Chaos Insurgency near by.
Juggernaut Chaos | 39 | Give Candy Ability | During a Chaos Insurgency Respawn Wave | A Chaos Member that specializes in explosives.
Chaos Insurgency Spy | 40 | Disguised, Remove Disguise | During a MTF Respawn Wave | A Chaos Member that is disguised as an MTF Member.
MTF Wisp | 41 | Wisp | During a MTF Respawn Wave | A MTF Member that can go through doors, but has reduced sprint and some item limitations.

# Snivy's Custom Roles Abilities
This contains Joker's original custom roles abilities as well

### Ability List
Below is a list of every ability (currently) with a short discription of what it does

Custom Ability | AbilityName | Ability Type | Description
:---: | :---: | :---: | :------
Active Camo | ActiveCamo | Active Ability | For a set amount of time, allows the player to go invisible unless they fire their weapon, opening/closing doors will reapply the effect.
Custom Role Escape | CustomRoleEscape | Passive Ability | When a player that has this ability tries to escape, you can give them a set custom role.
Charge | ChargeAbility | Active Ability | Charges towards a location.
Detect | Detect | Active Ability | Detects any hostiles of the Chaos Insurgency nearby.
Disguised | Disguised | Passive Ability | This handles all things related to being disguised, such as preventing accidental friendly fire.
Door Picking Ability | DoorPicking | Active Ability | When activated, the player is able to open a door.
Dwarf Ability | DwarfAbility | Passive Ability | This handles everything in regards to being a dwarf, size, stamina usage, and some item restrictions.
Flipped Ability | Flipped | Passive Ability | This handles everything in regards to being vertically flipped.
Giving Candy Ability | GivingCandyAbility | Passive Ability | Gives candy that's listed at spawn.
Healing Mist | HealingMist | Active Ability | Activates a short term healing AOE effect.
Heal on Kill | HealOnKill | Passive Ability | Heals on kill, hopefully self explainitory on what that does.
Martyrdom | Martyrdom | Passive Ability | Explosive death.
Reduced Movement Speed | MoveSpeedReduction | Passive Ability | Makes the player moves slower.
Reactive Hume Shield | ReactiveHume | Passive Ability | A Hume Shield that builds up, that reduces incoming damage.
Remove Disguise | RemoveDisguise | Active Ability | The ability to remove their disguise, I.E. If MTF, become CI, and vise versa.
Restricted Escape | RestrictedEscape | Passive Ability | This just restricts player escapes for custom roles that has this ability.
Restricted Items | RestrictedItem | Passive Ability | This allows a specific set of restricted items. This is usually complemented with other abilities.
Speed On Kill | SpeedOnKill | Passive Ability | Gives a speed boost on kill.
Wisp | Wisp | Passive Ability | Gives the ability to go through doors, but at the cost of not being able to see as far and a reduced sprint time.

# Snivy's Custom Items

A collection of custom items that I have made over the time (plus one from Jamwolff)

Item Name | Item Type | ItemID | Spawn Locations | Description
:---: | :---: | :---: | :---: | :------
Obscurus Veil-5 | Flash Bang | 20 | 25% Chance in HCZ Armory, GR18, Surface Nuke Room, LCZ Armory, Nuke Armory, Spawn Limit: 5 | When thrown, causes a permament smoke cloud to appear at place of detonation.
Explosive Round Revolver | Revolver | 21 | 10% Chance in MicroHID, HCZ Armory, 096s Room, 20% Chance in Nuke Armory and 049's Armory, Spawn Limit: 1 | When used, bullets becomes short fused grenades at point of impact they land.
Nerve Agent Grenade | Flash Bang | 22 | 25% Chance in LCZ Armory, HCZ Armory, Nuke Armory, 049 Armory, and Surface Nuke Room, Spawn Limit: 2 | When thrown, causes a customizable duration nerve agent that causes damage when walked through.
Phantom Decoy Device | Adrenline | 23 | 25% Chance to spawn in a random locker, Spawn Limit: 1 | When used, teleports the player randomly to a different room while dropping a fake corspe. Giving massive debuffs when used.
Phantom Lantern | Lantern | 24 | 10% Chance to spawn in MicroHID, 096s room, GR18, 106s Room, HczTestRoom, Spawn Limit: 1 | When toggled, makes the player go incredibly slowly, become invisible, and walk through doors, while locking out the inventory.
Explosive Resistant Armor | Heavy Armor | 25 | 25% Chance to spawn in MicroHID, HCZ Armory, 049 Armory, Spawn Limit: 1 | When equipped, makes the user more resistant to explosives.
KY Syringe | Adrenline | 42 | 100% Chance to spawn in 096s room, Spawn Limit: 1 | When used, the player dies.

# Snivy's Server Events

A plugin meant to add some togglable events that adds onto the normal round structure

Currently the events part of the plugin is provided below
- Blackout
- SCP 173 Infection (when 173 kills someone, they become 173)
- SCP 173 Hydra (when 173 dies, they respawn with another player both as 173, which are smaller)
- Variable Lights
- Short Players Event
- Chaos Event
- Name Redacted Event
- Freezing Temperatures Event

Exiled Permission: vvevents.run

RA Command: vve

RA Sub Commands: Blackout, Chaotic, FreezingTemps, NameRedacted, 173Hydra, 173Infection, ShortPeople, VariableLights

Feel free to fork and contribute to this plugin.

This plugin is not designed to add new gamemodes that takes place instead of the main gameplay loop, these events are meant to add to the main gameplay loop.

# Credits
@Mostly-Lucid for helping a lot with the SCP-173 Hydra event (and by that I mean doing it entirely because I had a very smooth brain moment)

@Jamwolff for the Short Players Event

# Micro Damage Reduction
Allows for a configurable damage reduction to what ever class is specificed in the Config.

# Surface Final Escape Door Opener
It opens Surface's Final Escape Door.

# Micro Evaporates Players
When the MicroHID kills a player, they evaporate instead of flopping onto the floor.

# Flamingo Adjustments
For the Christmas Event, when SCP-1507 is on the field, their damage and damage multiplayer can be adjusted.

# SCP-1576 Spectator Viewer
When SCP-1576 is used, it will tell the user how many people are in spectator (but not who) and how long before the next respawn wave.
Use %spectators% to get the spectators.
Use %timebeforespawnwave% to get the amount of time remaining before the respawn wave.

# Voting Commands
Adds a simple voting command for players to vote on stuff.
StartVote (or sv) to start a vote, this is in the RA Panel or Game Console
.vote to vote for an option
