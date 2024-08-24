using Godot;
using System;
using System.Collections.Generic;

public partial class Fase : Node3D
{
	
	[Signal]
	public delegate void TerminouFaseEventHandler();

	[ExportGroup("Informações da fase")]

	[Export]
	public String Titulo { get; set; }

	[Export(PropertyHint.MultilineText)]
	public String Descricao { get; set; }

	[Export(PropertyHint.MultilineText)]
	public Color CorFundo { get; set; }


	[Export]
	public PackedScene ProximaFase { get; set; }

	[ExportGroup("Propriedades da fase")]
	[Export]
	public Marker3D SpawnPlayer { get; set; }
	
	[Export]
	public jogador Player { get; set; }

	[Export]
	public Area3D AreaVitoria { get; set; }

	[Export]
	public Godot.Collections.Array<Node3D> Moedas { get; set; }

	public override void _Ready()
	{
		if (Player != null) {
			Player.Morreu += () => _on_jogador_morreu();
		}

		if (AreaVitoria != null) {
			AreaVitoria.BodyEntered += (body) => _on_area_3d_body_entered(body);
		}

		foreach (Node3D moeda in Moedas) {
			moeda.TreeExiting += () => jogador_pegou_moeda(moeda);
		}
	}

	private void jogador_pegou_moeda(Node3D moeda) {
		Moedas.Remove(moeda);
	}

	private void _on_jogador_morreu()
	{
		Player.GlobalPosition = SpawnPlayer.GlobalPosition;
		Player.Resetar();
	}

	private void _on_area_3d_body_entered(Node3D body)
	{
		GD.Print("Checar se terminou fase: ", Moedas.Count);
		if (Moedas.Count != 0) {
			return;
		} 

		EmitSignal("TerminouFase");
		GD.Print("Terminou!");
	}
}