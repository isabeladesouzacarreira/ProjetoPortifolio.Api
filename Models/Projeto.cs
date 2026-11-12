namespace ProjetoPortifolio.Api.Models
{
    public class Projeto
    {
        public int Id_projeto { get; set; }
        public string nome_projeto { get; set; }
        public string imagem_video { get; set; }
        public string descricao { get; set; }
        public string habilidades { get; set; }
        public string ferramentas { get; set; }
        public string Tec_utilizadas { get; set; }
        public int id_aluno { get; set; }
    }
}
