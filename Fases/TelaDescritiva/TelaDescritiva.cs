using Godot;
using System;

public partial class TelaDescritiva : Control
{
	[Signal]
	public delegate void ContinuarPressionadoEventHandler();
	
	[Export]
	public Color BackgroundColor { get; set; }
	[Export]
	public Label tituloLabel { get; set; }
	[Export]
	public Label DescricaoLabel { get; set; }
	[Export]
	public ColorRect Background { get; set; }


	public void mostrarTela(string titulo, string descricao, Color color) {
		tituloLabel.Text = titulo;
		DescricaoLabel.Text = descricao;
		Background.Color = color;
	}

	private void _on_continuar_pressed()
	{
		EmitSignal("ContinuarPressionado");
	}

}
