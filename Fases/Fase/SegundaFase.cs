using Godot;
using System;

public partial class SegundaFase : Fase
{

	private void _on_jogador_morreu()
	{
		Player.GlobalPosition = SpawnPlayer.GlobalPosition;
		Player.Resetar();
	}

	private void _on_area_3d_body_entered(Node3D body)
	{
		EmitSignal("TerminouFase");
		GD.Print("aqui?aaaaaa");
	}
}
