using Godot;
using System;
using System.Threading.Tasks;

public partial class GerenciadorDeFases : Node
{
	[Export] public PackedScene primeiraFase {get; set;}
	[Export] public TelaDescritiva telaDescritiva {get; set;}
	[Export] public Control telaFinal {get; set;}

	public override void _Ready()
	{
		telaDescritiva.Visible = false;
		telaFinal.Visible = false;
		LoopDeFases();
	}
	
	public async void LoopDeFases() 
	{
		Fase fase = primeiraFase.Instantiate() as Fase;

		while (fase != null) {
			await MostrarTelaDescritiva(fase.Titulo, fase.Descricao, fase.CorFundo);

			GetTree().Root.CallDeferred("add_child",fase);

			await ToSignal(fase,"TerminouFase");

			GetTree().Root.CallDeferred("remove_child",fase);

			fase = fase.ProximaFase != null ? fase.ProximaFase.Instantiate<Fase>() : null;
		}

		telaFinal.Visible = true;
	}

	public async Task MostrarTelaDescritiva(string titulo, string descricao, Color CorFundo) {
		telaDescritiva.Visible = true;

		telaDescritiva.mostrarTela(titulo, descricao, CorFundo);
		await ToSignal(telaDescritiva,"ContinuarPressionado");

		telaDescritiva.Visible = false;
	}

	public override void _Process(double delta)
	{
	}
}
