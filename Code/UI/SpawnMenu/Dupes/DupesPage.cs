using Sandbox.UI;

/// <summary>
/// This component has a kill icon that can be used in the killfeed, or somewhere else.
/// </summary>
[Title( "#spawnmenu.tab.dupes" ), Order( 3000 ), Icon( "✌️" )]
public class DupesPage : BaseSpawnMenu
{
	protected override void Rebuild()
	{
		AddHeader( "#spawnmenu.section.local" );
		AddOption( "📂", "#spawnmenu.dupes.local", () => new DupesLocal() );
	}

	protected override void OnMenuFooter( Panel footer )
	{
		footer.AddChild<DupesFooter>();
	}
}

public enum DupeCategory
{
	[Icon( "🚗" )]
	Vehicle,
	[Icon( "🤖" )]
	Robot,
	[Icon( "✈️" )]
	Plane,
	[Icon( "🕺🏼" )]
	Pose,
	[Icon( "🏹" )]
	Weapon,
	[Icon( "🖼️" )]
	Art,
	[Icon( "🏠" )]
	Scene,
	[Icon( "🎳" )]
	Game,
	[Icon( "🛸" )]
	Spaceship,
	[Icon( "🎰" )]
	Machine,
	[Icon( "🧸" )]
	Toys,
	[Icon( "🪤" )]
	Trap,
	[Icon( "⛵" )]
	Boat,
	[Icon( "📂" )]
	Other
}

public enum DupeMovement
{
	Static,
	Wheeled,
	Flying,
	Walking,
	Water,
	Tracked
}
