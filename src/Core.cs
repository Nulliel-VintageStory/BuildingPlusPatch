using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

[assembly: ModInfo(
	"BuildingPlusPatch",
	Authors     = new []{ "nulliel" } )]
namespace BuildingPlusPatch;

public class Core : ModSystem
{
	public Patcher patcher;

	public override void Start(ICoreAPI api)
	{
		base.Start(api);

		patcher = new Patcher("nulliel.buildingpluspatch");
		patcher.PatchAll();
	}

	public override void Dispose()
	{
		patcher.Dispose();

		base.Dispose();
	}
}
