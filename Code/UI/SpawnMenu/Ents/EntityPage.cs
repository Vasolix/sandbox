
/// <summary>
/// This component has a kill icon that can be used in the killfeed, or somewhere else.
/// </summary>
[Title( "#spawnmenu.tab.entity" ), Order( 2000 ), Icon( "🧠" )]
public class EntityPage : BaseSpawnMenu
{
	static Dictionary<string, string> CategoryIcons = new()
	{
		{ "Chair", "🪑" },
		{ "Pickup", "🧰" },
		{ "Weapon", "🔫" },
		{ "Npc", "🤖" },
		{ "Vehicle", "🚕" },
		{ "World", "🌍" },
	};

	protected override void Rebuild()
	{
		AddHeader( "#spawnmenu.section.local" );

		var categories = ResourceLibrary.GetAll<ScriptedEntity>()
			.Where( e => !e.Developer || ServerSettings.ShowDeveloperEntities )
			.Select( e => string.IsNullOrWhiteSpace( e.Category ) ? "Other" : e.Category )
			.Distinct()
			.OrderBy( c => c == "Other" ? "\xFF" : c ); // sort Other last

		foreach ( var category in categories )
		{
			var cat = category; // capture for lambda
			var icon = CategoryIcons.GetValueOrDefault( cat, "📦" );
			AddOption( icon, cat, () => new EntityListLocal { Category = cat } );
		}

	}
}
