[Alias( "basewars_printer" )]
public sealed class BaseWarsPrinter : Component, Component.IDamageable, Component.IPressable
{
	[Property, Sync( SyncFlags.FromHost )]
	public PlayerData OwnerData { get; set; }

	[Property, Sync( SyncFlags.FromHost )]
	public int StoredMoney { get; private set; }

	[Property, Range( 1, 1000 ), Step( 1 )]
	public int PrintAmount { get; set; } = 25;

	[Property, Range( 1, 120 ), Step( 1 )]
	public float PrintInterval { get; set; } = 5f;

	[Property, Range( 1, 500 ), Sync( SyncFlags.FromHost )]
	public float Health { get; set; } = 100f;

	private TimeSince _timeSincePrint;
	private TextRenderer _displayText;
	private int _lastDisplayedMoney = int.MinValue;

	protected override void OnStart()
	{
		EnsureDisplay();
		UpdateDisplayText();
	}

	protected override void OnUpdate()
	{
		EnsureDisplay();
		UpdateDisplayText();

		if ( IsProxy ) return;
		if ( !OwnerData.IsValid() ) return;
		if ( _timeSincePrint < PrintInterval ) return;

		_timeSincePrint = 0;
		StoredMoney += PrintAmount;
	}

	private void EnsureDisplay()
	{
		if ( _displayText.IsValid() ) return;

		var screen = GameObject.Children.FirstOrDefault( x => x.Name == "Printer Screen" );
		if ( !screen.IsValid() )
		{
			screen = new GameObject( GameObject, false, "Printer Screen" );
			screen.LocalPosition = new Vector3( 18.5f, 0f, 10f );
			screen.LocalRotation = Rotation.FromYaw( 90f );
			screen.LocalScale = new Vector3( 0.28f, 0.18f, 1f );

			var panel = screen.AddComponent<ModelRenderer>();
			panel.Model = Model.Load( "models/dev/plane_blend.vmdl" );
			panel.MaterialOverride = Material.Load( "materials/dev/reflectivity_30.vmat" );
			panel.Tint = Color.Black;
		}

		var text = screen.Children.FirstOrDefault( x => x.Name == "Printer Screen Text" );
		if ( !text.IsValid() )
		{
			text = new GameObject( screen, false, "Printer Screen Text" );
			text.LocalPosition = new Vector3( 0f, 0f, 1f );
			text.LocalRotation = Rotation.Identity;
		}

		_displayText = text.GetOrAddComponent<TextRenderer>();
		_displayText.HorizontalAlignment = TextRenderer.HAlignment.Center;
		_displayText.VerticalAlignment = TextRenderer.VAlignment.Center;
		_displayText.FontFamily = "Poppins";
		_displayText.FontSize = 42f;
		_displayText.FontWeight = 800;
		_displayText.Scale = 0.25f;
		_displayText.Color = Color.Green;
	}

	private void UpdateDisplayText()
	{
		if ( !_displayText.IsValid() ) return;
		if ( _lastDisplayedMoney == StoredMoney ) return;

		_lastDisplayedMoney = StoredMoney;
		_displayText.Text = $"${StoredMoney}";
	}

	bool Component.IPressable.CanPress( IPressable.Event e )
	{
		return StoredMoney > 0;
	}

	bool Component.IPressable.Press( IPressable.Event e )
	{
		Collect( e.Source.GameObject );
		return true;
	}

	IPressable.Tooltip? Component.IPressable.GetTooltip( IPressable.Event e )
	{
		if ( StoredMoney <= 0 )
			return new IPressable.Tooltip( "Printer", "payments", "Aucun argent a recuperer" );

		return new IPressable.Tooltip( "Recuperer l'argent", "payments", $"${StoredMoney}" );
	}

	[Rpc.Host]
	private void Collect( GameObject presserObject )
	{
		if ( !presserObject.IsValid() ) return;

		var player = presserObject.Root.GetComponent<Player>();
		if ( !player.IsValid() || !player.PlayerData.IsValid() ) return;

		if ( player.PlayerData != OwnerData )
		{
			Sandbox.UI.Notices.SendNotice( player.Network.Owner, "block", Color.Red, "Ce printer ne t'appartient pas.", 3 );
			return;
		}

		if ( StoredMoney <= 0 )
		{
			Sandbox.UI.Notices.SendNotice( player.Network.Owner, "payments", Color.Yellow, "Le printer n'a rien produit pour le moment.", 3 );
			return;
		}

		var amount = StoredMoney;
		StoredMoney = 0;
		OwnerData.AddMoney( amount );

		Sandbox.UI.Notices.SendNotice( player.Network.Owner, "payments", Color.Green, $"+${amount} recupere du printer.", 3 );
	}

	void Component.IDamageable.OnDamage( in DamageInfo damage )
	{
		if ( IsProxy ) return;

		Health -= damage.Damage;

		if ( Health <= 0 )
		{
			GameObject.Destroy();
		}
	}
}
