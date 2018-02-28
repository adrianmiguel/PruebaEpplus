using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading.Tasks;

namespace PruebaEPPlus.Entradas
{
    class LeerExcel
    {
        public void LeerExcelXLSX()
        {
            var appSettings = ConfigurationManager.AppSettings;

            string RutaEntradas_BB = appSettings["RutaSalida"];
            string ExtensionArchivoEntrada = ".xlsx";//appSettings["ExtensionExcelEntrada"];

            DateTime FechaActual = DateTime.Now;
            string Dia = FechaActual.ToString("dd");
            string Mes = FechaActual.ToString("MM");
            string Anio = FechaActual.ToString("yyyy");
            string Ruta_Archivo = "";
            string CadenaConexionArchivoExcel = "";

            if (ExtensionArchivoEntrada == ".xls")
            {
                Ruta_Archivo = Path.Combine(RutaEntradas_BB, "xlExcel7.xls");
                CadenaConexionArchivoExcel = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Ruta_Archivo + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=2'";
            }
            else if (ExtensionArchivoEntrada == ".xlsx")
            {
                Ruta_Archivo = Path.Combine(RutaEntradas_BB, "xlExcel7.xls");
                CadenaConexionArchivoExcel = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Ruta_Archivo + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            string Query = "SELECT [Caso], [Numero POLIZA], [ASEGURADO], [RIESGO], [Nombre Arhivo Poliza] FROM [Poliza$]";
            //string Query = "SELECT * FROM [Hoja1$]";
            OleDbConnection con = new OleDbConnection(CadenaConexionArchivoExcel);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            OleDbCommand cmd = new OleDbCommand(Query, con);
            OleDbDataAdapter Adaptador = new OleDbDataAdapter();
            Adaptador.SelectCommand = cmd;

            DataSet ds = new DataSet();
            Adaptador.Fill(ds);
            Adaptador.Dispose();
            con.Close();
            con.Dispose();


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string Caso = dr["Caso"].ToString();
                string Poliza = dr["Numero POLIZA"].ToString();
                string Asegurado = dr["ASEGURADO"].ToString();
                string Riesgo = dr["RIESGO"].ToString();
                string NombreArchivo = dr["Nombre Arhivo Poliza"].ToString();
            }
        }
        }
}
