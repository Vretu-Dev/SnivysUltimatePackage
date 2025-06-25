Snivy's Ultimate Package contains all of the plugins I have made into one mega plugin for SCP SL.

I do mean made, any other plugins that I have ported or currently maintain, such as Serpents Hand, UIU Rescue Squad, etc is not included here.

![Downloads](https://img.shields.io/github/downloads/SnivyFilms/SnivysUltimatePackage/total.svg)

# Installing this plugin
> [!IMPORTANT]
> In releases, there is 2 versions available, `SnivysUltimatePackage.dll` and `SnivysUltimatePackageOneConfig.dll`. I would advise using `SnivysUltimatePackage.dll`, due to how many config options there are, mainly with Custom Items and Roles, `SnivysUltimatePackage.dll` seperates each of their configs into a file per section. (I.E. all the custom item config stuff is together and its just that in a file). Some servers may not play nice with `SnivysUltimatePackage.dll` and may throw null pointer errors and things may not work as intended. If this happens, you can try `SnivysUltimatePackageOneConfig.dll` which throws everyone into one config, either your `(port)-config.yml` (Exiled Combined Configs) or `Plugins/VVUltimatePluginPackageOneConfig/(port).yml` (Exiled Seperated Configs)

> [!CAUTION]
> There is a check now that detects if both plugin versions are on the server at the same time. If the server starts and both are present, both plugins will disable. This is due to both `SnivysUltimatePackage.dll` and `SnivysUltimatePackageOneConfig.dll` will try to race eachother for enabling stuff. Just remove one of them from your server and restart.

> [!CAUTION]
> There is an additional plugins, `SnivysFreeCustomRoles` this will be functionally unsupported and only made for the people who want extra custom role slots for customization. This should work with either parent plugin out of the box.

# Plugin List:

Snivy's Custom Roles

Snivy's Custom Roles Abilities

Snivy's Server Events

Micro Damage Reduction

-------------------------------------------------

New to this plugin is also:

Snivy's Custom Items

Surface Final Escape Door Opener

Flamingo Adjustments

Micro Evaportate Players

SCP 1576 Spectator Viewer

Voting Commands

Server Specific Setting System

# Snivy's Custom Roles
### Role List
Below is a table of all the current custom roles, followed by a breif description of them. Many of them rely on special abilities also added by this plugin, refer to the list of abilities for more details about what each does. This is default settings and can be customized by the server owner.

Note this does not include the 20 free custom roles that is included, there will not be included in the list, there will also be no support for them

RoleName | RoleID | Abilities | Spawn Type | Description
:---: | :---: | :---: | :---: | :------
Containment Engineer | 30 | Restricted Escape | Immediately when a round begins | A Scientist is randomly selected and is set into Enterance Zone with a Containment Engineer Keycard.
Protocol Enforcer | 31 | None | Immediately when a round begins | A lighter facility guard that spawns in light containment zone. They spawn with a Tranquilizer, Medkit, Painkillers, Radio, Light Armor, and a Zone Manager Keycard.
Biochemist| 32 | Healing Mist, Martyrdom, Custom Role Escape | Immediately when a round begins | A Scientist genetically altered.
Containment Guard | 33 | None | Immediately when a round begins | A Facility Guard specializing in recontaining SCPs.
Border Patrol | 34 | None | Given by Admin Command only | A facility guard specialized in ensuring safe passage from Enterance and Heavy Checkpoints.
Nightfall | 35 | Data Missing | Data Missing | Data Missing.
A7 Chaos | 36 | None | During a Chaos Insurgency Respawn Wave | A Chaos Member that spawns with an A7.
Flipped | 37 | Scale Ability | Given by Admin Command only | For those people who complains about dwarfs when they spawn in as it.
Telepathic Chaos | 38 | Detect | During a Chaos Insurgency Respawn Wave | A Chaos Member that can detect hostiles to the Chaos Insurgency near by.
Juggernaut Chaos | 39 | Give Candy Ability | During a Chaos Insurgency Respawn Wave | A Chaos Member that specializes in explosives.
Chaos Insurgency Spy | 40 | Disguised, Remove Disguise | During a MTF Respawn Wave | A Chaos Member that is disguised as an MTF Member.
MTF Wisp | 41 | Effect Enabler | During a MTF Respawn Wave | A MTF Member that can go through doors, but has reduced sprint and some item limitations.
Ballistic SCP-049-2 | 42 | Martyrdom | Chance during revive from SCP-049 | A zombie that goes boom on death.
Dwarf SCP-049-2 | 43 | Scale Ability | Chance during revive from SCP-049 | A smaller zombie.
Chaos Phantom | 44 | Active Camo | Immediately when a round begins | A Chaos Insurgent that takes place of a guard, can go invisible.
Medic SCP-049-2 | 45 | Healing Mist, Effect Enabler | Chance during revive from SCP-049 | A zombie that can heal other SCPs at the cost of being a bit more slow
Lock-picker Class D | 46 | Door Picking | Immediately when a round begins | A Class D that used to be a lock picker, can open some keycard doors if they dont have the keycard.
MTF Demolitionist | 47 | None | During a MTF Spawn Wave | A MTF Member that specializes in explosives.
Vanguard | 48 | None | During a MTF Spawn Wave | A MTF Member with an alternative loadout, being able to mark a target to do recieve more damage for a short time.
Theoretical Physicist Scientist | 49 | Pocket Dimension Escape Chance, Custom Role Escape | Immediately when a round begins | A Scientist that believes that the Pocket Dimension has a lot more. Has 2 guaranteed escapes from Pocket Dimension to SCP-106's room.
MTF Paramedic | 50 | Healing Mist, Reviving Mist | Chance during a MTF Spawn Wave | A MTF member that specializes in healing teammates.
Class-D Analyst | 51 | Detect, Custom Role Escape | Immediately when a round begins | A Class D that has some analysis abilities.
Class-D Tank | 52 | Effect Enabler, Scale Ability, Custom Role Escape | Immediately when a round begins | A Class-D that can take a bit more punishment than other Class-D.
Infected Zombie | 53 | Team Convert On Kill | Chance during revive from SCP-049 | A zombie than can convert targets to a SCP-049-2 when killing a player.
Poisonous Zombie | 54 | Apply Effect On Hit | Chance during revive from SCP-049 | A zombie than when attacking a player, will apply the poisoned effect for 10 seconds.
Speedster Zombie | 55 | Effect Enabler | Chance during revive from SCP-049 | A zombie that is faster than others.
Husk | 56 | Apply Husk Infection On Hit | Guaranteed when a player doesn't save themselves from a Husk Infection | When a Husk takes over the body, a random spectator (if the player was alive when the Husk takes over) takes control of the player OR the player (if the player is dead) revives into a Husk.


# Snivy's Custom Roles Abilities
This contains Joker's original custom roles abilities as well

### Ability List
Below is a list of every ability (currently) with a short discription of what it does.

Custom Ability | AbilityName | Ability Type | Description
:---: | :---: | :---: | :------
Active Camo | ActiveCamo | Active Ability | For a set amount of time, allows the player to go invisible unless they fire their weapon, opening/closing doors will reapply the effect.
Ability Remover | AbilityRemover | Passive Ability | Clears abilities, helpful if you have multiple plugins with custom roles and some custom role abilities are given to the wrong custom role.
Apply Effect On Hit | ApplyEffectOnHit | Passive Ability | When a player with this ability attacks another player, it will give them an effect.
Apply Husk Infection On Hit | ApplyHuskInfection | Passive Ability | When a player with this ability attacks another player, it gives them a chance to give them a husk infection.
Custom Role Escape | CustomRoleEscape | Passive Ability | When a player that has this ability tries to escape, you can give them a set role or custom role.
Charge | ChargeAbility | Active Ability | Charges towards a location.
Detect | Detect | Active Ability | Detects any hostiles of the player's role nearby.
Disguised | Disguised | Passive Ability | This handles all things related to being disguised, such as preventing accidental friendly fire.
Door Picking Ability | DoorPicking | Active Ability | When activated, the player is able to open a door.
Effect Enabler Ability | EffectEnabler| Passive Ability| Handles giving effects to players
Giving Candy Ability | GivingCandyAbility | Passive Ability | Gives candy that's listed at spawn.
Healing Mist | HealingMist | Active Ability | Activates a short term healing AOE effect.
Heal on Kill | HealOnKill | Passive Ability | Heals on kill, hopefully self explainitory on what that does.
Infinite Stamina | InfiniteStamina | Passive Ability | Gives the player infinite stamina.
Life Steal | LifeSteal | Passive Ability | When dealing damage, the attacker gains back a % of health.
Martyrdom | Martyrdom | Passive Ability | Explosive death.
Pocket Dimension Escape Chance | PocketDimensionEscapeChance | Passive Ability | Can configure a custom escape chance from the pocket dimension.
Reactive Hume Shield | ReactiveHume | Passive Ability | A Hume Shield that builds up, that reduces incoming damage.
Remove Disguise | RemoveDisguise | Active Ability | The ability to remove their disguise, I.E. If MTF, become CI, and vise versa.
Restricted Escape | RestrictedEscape | Passive Ability | This just restricts player escapes for custom roles that has this ability.
Restricted Items | RestrictedItem | Passive Ability | This allows a specific set of restricted items. This is usually complemented with other abilities.
Reviving Mist | RevivingMist | Active Ability | Allows the user to revive other players that recently died.
Scale Ability | ScaleAbility | Passive Ability | This sets a players scale.
Speed On Kill | SpeedOnKill | Passive Ability | Gives a speed boost on kill.
Team Convert On Kill | TeamConvertOnKill | Passive Ability | When a player kills another, they will be converted to the player's side.

# Snivy's Custom Items

A collection of custom items that I have made over the time (plus one from Jamwolff). This is default settings and can be customized by the server owner.

Item Name | Item Type | ItemID | Spawn Locations | Spawn Limit | Description
:---: | :---: | :---: | :---: | :---: | :------
Obscurus Veil-5 | Flash Bang | 20 | 25% Chance in HCZ Armory, GR18, Surface Nuke Room, LCZ Armory. | 5 | When thrown, causes a permament smoke cloud to appear at place of detonation.
Explosive Round Revolver | Revolver | 21 | 10% Chance in MicroHID, HCZ Armory, 096s Room, 20% Chance in 049's Armory and 079's Armory. | 1 | When used, bullets becomes short fused grenades at point of impact they land.
Nerve Agent Grenade | Flash Bang | 22 | 25% Chance in LCZ Armory, HCZ Armory, 049 Armory, 079's Armory, and Surface Nuke Room. | 2 | When thrown, causes a customizable duration nerve agent that causes damage when walked through.
Phantom Decoy Device | Adrenline | 23 | 25% Chance to spawn in a random locker, 939's room, and Heavy Test Room. | 1 | When used, teleports the player randomly to a different room while dropping a fake corspe. Giving massive debuffs when used.
Phantom Lantern | Lantern | 24 | 10% Chance to spawn in MicroHID, 096s room, GR18, 106s Room, HczTestRoom. | 1 | When toggled, makes the player go incredibly slowly, become invisible, and walk through doors, while locking out the inventory.
Explosive Resistant Armor | Heavy Armor | 25 | 25% Chance to spawn in MicroHID, HCZ Armory, 049 Armory. | 1 | When equipped, makes the user more resistant to explosives.
LJ-429 | Adrenline | 26 | 100% Chance to spawn in 096s room. | 1 | When used, the player dies.
Phantom Pulse [Disabled] | FSP-9 | 27 | 25% Chance in Gate A, Gate B, GR-18. | 1 | When firing at friendlies, it will heal them, applying AHP if they have full health, when fired at zombies, gives them a chance to be revived back to a human role.
Silent Serenade | COM-18 | 28 | 20% Chance in 173 Armory, GR-18, 096 Room, PC-15. | 1 | When fired at someone, they will become tranquilized, SCPs have a resistance chance for it not to work, applying multiple tranquilizations will make the target more resistant to it.
SCP-1499 | SCP-268 | 29 | 10% Chance in MicroHID, 079's Room, In the escape hall building. | 1 | When putting on, will teleport you to a fixed point on the map for a specified amount of time.
PB-42 | Flash Bang | 30 | 50% Chance in Surface Nuke Room, HCZ Armory, 096s Room. | 5 | When throwing, will open doors and disconnect SCP-079 if 079 is at that camera.
Amnesioflux | SCP500 | 31 | 100% Chance in 096s room. | 1 | When consumed, removes you as a target from 096, just make sure you're not looking at him when you take it.
C4 | Grenade | 32 | 10% Chance in Light Armory, 25% Chance in Heavy Armory, 50% Chance in 049 Armory and 079's Armory, 100% Chance in Surface Nuke | 5 | A grenade that can be remotely detonated, requires a radio to detonate (by base).
SCP-2818 | E11SR | 33 | 10% Chance in MicroHID, 079's Armory | 1 | When fired, you are the bullet. This will kill the shooter but will do a lot of damage to the target. Note due to SL's Anticheat, the player's ragdoll wont be on the target, sorry :<.
Infinite Pills | Painkillers | 34 | 50% Chance to appear in a Misc Locker, 939's Room, Heavy Test Room | 3 | When consumed, the pills dont go away, it wont heal you however. This is entirely a joke item.
Cluster Grenade | Grenade | 36 | 15% Chance to appear in Light Armory, Heavy Armory, 049/173 Armory, Surface Nuke, 079's Armory | 1 | When exploding, it spawns more grenades.
Additional Health 207 | Anti SCP-207 | 37 | 25% Chance to appear in MicroHID, 079's Armory, 106's Room | 1 | When consumed, it adds more health to the player until they die.
Nebula Carapace | Light Armor | 38 | 25% Chance in GR-18 Glass Box, Heavy Armory, 049's Armory, Old 173's Room at the bottom of the stairs, 939's room, PC-15 room | 1 | The armor is so light and makes the user feel lighter, as it gives the user to have lower gravity.
Viper PDW | Crossvec | 39 | Spawns on MTF Vanguard, 25% Chance to appear in Heavy Armory, 049 Armory, Inside 096 Containment Chamber, Inside 079's Armory | 1 | A firearm that does different damage at different ranges.
Pathfinder | COM-18 | 40 | Spawns on MTF Vanguard | 1 | A firearm that when hitting a foe, causes them to take increased damage for a short amount of time.
X-57 Helios Beam | E11SR | 41 | 20% Chance to appear in MicroHID Room, 079's Armory | 1 | It's an E11 but it fires lasers!
Multibang | Flash Bang | 42 | 15% Chance in Light Armory, Heavy Armory, 049's Armory, 079's Armory, Surface Nuke Room | 2 | When the initial detonates, the spawns others to detonate after a bit of time.
Pathfinder Grenade | Flash Bang | 43 | 15% Chance in MicroHID, Guaranteed to spawn on MTF Vanguard | 1 | When detonates, spawns lines to any players in the area.
Prototype Keycard Basic | Custom Keycard | 44 | 0% Chance in normal play, Guaranteed to spawn in MicroHID, Heavy Armory, 049's Armory, GR18, 106's Room, 330's Control Room, or 173's Gate Room during Operation Crossfire | 1 | A Keycard needed to be collected and refined into the Prototype Keycard Refined during Operation Crossfire.
Prototype Keycard Refined | Custom Keycard | 45 | Guaranteed to spawn when upgrading Prototype Keycard Basic in 914 | 0 | The upgraded keycard from 914 for Operation Crossfire.
Portable Intercom | Radio | 47 | 10% Chance to spawn in MicroHID, 096's Room, GR-18, 106's Room, and Heavy Test Room | 1 | When turned on/off, it will grant global intercom to the user.
Husk Grenade | Flash Bang | 48 | 10% Chance to spawn in MicroHID, Heavy Armory, 049's Armory, 096's Room, 079's Armory | 5 | When detonated, it will give every human a husk infection if in range of detonation.
Calyxanide | Adrenline | 49 | 50% Chance to spawn in 939's Room, Heavy Armory, 049's Armory, 079's Armory | 2 | When used, it will cure a player of a Husk Infection.

> [!IMPORTANT]
> In regards to Apply Husk on Hit Custom Ability and Husk Grenade, `Custom Items`, `Custom Roles`, and `Custom Abilities` MUST be enabled to work. Otherwise the Husk Infection will not work.

# Snivy's Server Events

A plugin meant to add some togglable events, mostly to add to a round and not to replace a round

Currently the events part of the plugin is provided below

Event Name | Event Type | Description
:---: | :---: | :------
After Hours | Non-disruptive | Its like the facility is after hours, everything is functionally in a low power mode.
Blackout | Non-disruptive | Turns off the lights in the facility, restores them either at round end or if all generators are activated.
SCP 173 Infection | Non-disruptive | When 173 Kills someone, they become 173 as well.
SCP 173 Hydra | Disruptive | When 173 Dies, they respawn as 173, along with another person, reducing their size and health count.
Variable Lights | Non-disruptive | Activates disco mode to the facility (effectively).
Short People | Disruptive | Makes everyone short.
Chaos Event | Causes chaos in the facility, see below for what Chaos Event can do to a round.
Name Redacted | Non-disruptive | Removes everyones names.
Freezing Temperatures | Disruptive | The thermostat in the facility broke and its slowly freezing over, best to get out as fast as you can.
Snowballs Vs SCPs [Disabled] | Disruptive | Causes a snowball fight to start.
Operation Crossfire | Disruptive | A military simulator event where MTF, Scientists, and D-Class has goals they need to achieve, D-Class will be able to hold the Scientists hostage initially, as MTF needs to save them. MTF and Scientists have to upgrade a keycard and get out of the facility, as D-Class wants to hold the cards and stop MTF. This event cannot be started automatically by chance.
Low Gravity Facility | Non-disruptive | The facility has lower gravity. Making jumps for floaty

Chaos Event Functions, a lot of this can be customized to your hearts content.
Function | Description
:---: | :------
Item Steal | Steals everyones items
Item Give | Gives every human a random item (custom items included)
Random Teleport | Randomly Teleports each player to somewhere random
Fake Auto Nuke | Starts a fake auto nuke
Remove Weapons | Removes weapons from players
Give Random Weapons | Gives every human a random weapon
Death Match | Sets every players health to 1 HP
Enable Blackout | Starts the Blackout Event
Enable Freezing Temperatures | Starts Freezing Temperatures
Enable SCP 173 Hydra | Starts SCP 173 Hydra Event
Enable SCP 173 Infection | Starts SCP 173 Infection Event
Enable Short People | Starts the Short People Event
Enable Variable Lights | Starts Variable Lights
Enable Low Gravity | Starts Low Gravity Event
FBI Open Up | Picks a random, non foundation side player, then teleports every foundation member to them
Grenade Feet | Spawns a grenade at everyones feet
Unsafe Medical Items | Medical Items may cause more harm than good
Enable Name Redacted | Starts Name Redacted event
Fake Cassie Respawn Announcement | Makes a fake Cassie respawn announcement, with a random chance to be MTF, CI, UIU, or SH
Rapid Fire Teslas | Causes Tesla Gates to rapidly fire
Player Tauntrum | Causes all players to have SCP-173's tantrum spawn on them
Router Kicking Simulator | Gives players fake lag and teleports them back a distance after a bit of time
Super Speed | Makes all the players super speed
Drop Items | Drops all the items from a player's inventory

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

# Server Specific Settings System
Adds a simple (and questionably coded) Server Specifics Setting System.

# Commands
Command | Required Permission | Run Location | Description
:---: | :---: | :---: | :------
vve | vvevents | Remote Admin, Server Console | Shows the list of events that can be used
vve (some event name) | vvevents.rund | Remote Admin, Server Console | Starts disruptive events.
vve (some event name) | vvevents.runn | Remote Admin, Server Console | Starts non-disruptive events.
vve stop | vvevents.stop | Remote Admin, Server Console | Stops any active events.
startvote | vvvotes.start | Remote Admin, Server Console | Starts a vote.
.vote | None | Player Console | Responds to an active vote
.detonate | None | Player Console | Detonates C4, assuming requirements are met.

# Credits
@Mostly-Lucid for helping a lot with the SCP-173 Hydra event (and by that I mean doing it entirely because I had a very smooth brain moment)

@Jamwolff for the Short Players Event and LJ-429 Custom Item
