
/// <summary>
/// This component has a kill icon that can be used in the killfeed, or somewhere else.
/// </summary>
[Title( "#spawnmenu.tab.props" ), Order( 0 ), Icon( "📦" )]
public class PropsPage : BaseSpawnMenu
{
	protected override void Rebuild()
	{
		AddHeader( "#spawnmenu.section.local" );
		AddOption( "🧍", "#spawnmenu.props.all", () => new SpawnPageLocal() );
		AddOption( "🙎", "#spawnmenu.props.characters", () => new SpawnPageLocal() { Category = "characters" } );
		AddOption( "📦", "#spawnmenu.props.props", () => new SpawnPageLocal() { Category = "props" } );
	}
}
