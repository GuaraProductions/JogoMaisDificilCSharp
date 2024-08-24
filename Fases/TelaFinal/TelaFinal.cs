using Godot;
using System;

public partial class TelaFinal : Control
{
	public void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Menu/MenuPrincipal.tscn");
	}
}
