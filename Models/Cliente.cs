using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SistemaDeDeudas.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Cedula { get; set; }
        public decimal Deuda { get; set; }

        public string Producto { get; set; }


        


    }
}
