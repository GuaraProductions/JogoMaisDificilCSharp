using Godot;
using System;

public partial class MenuPrincipal : Control
{

	private void _on_jogar_pressed()
	{
		GetTree().ChangeSceneToFile("res://Fases/Fase/GerenciadorDeFases/GerenciadorDeFases.tscn");
	}

	private void _on_sair_pressed() {
		GetTree().Quit(0);
	}
}
